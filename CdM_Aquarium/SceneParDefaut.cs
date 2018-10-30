/*
  * Auteurs : Brunazzi Robin
  * Date : 18.09.2018
  * Projet : Cité des métiers
  * Description : 
  */

using CdM_Aquarium.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    class SceneParDefaut
    {
        #region Constantes
        const int HAUTEUR_AQUARIUM = 600;
        const int LARGEUR_AQUARIUM = 900;
        #endregion

        #region Champs
        private bool changer = true;

        private Form _vue;
        private Timer _minuterie;
        private Timer _rafraichir;
        private Random _rnd;
        private int _hauteurAquarium;
        private int _largeurAquarium;

        private List<Bulle> _bulles;
        private List<Bulle> _bullesASupprimer;
        private List<Bulle> _bullesAGonfler;
        private List<Poisson> _poissons;
        #endregion

        #region Propriétés
        /// <summary>
        /// Liste des bulles de l'aquarium
        /// </summary>
        private List<Bulle> Bulles { get => _bulles; set => _bulles = value; }
        private List<Poisson> Poissons { get => _poissons; set => _poissons = value; }
        public int HauteurAquarium { get => _hauteurAquarium; set => _hauteurAquarium = value; }
        public int LargeurAquarium { get => _largeurAquarium; set => _largeurAquarium = value; }
        public Form Vue { get => _vue; set => _vue = value; }
        public Timer Minuterie { get => _minuterie; set => _minuterie = value; }
        public Random Rnd { get => _rnd; set => _rnd = value; }

        /// <summary>
        /// Liste stockant les bulles à supprimer
        /// </summary>
        public List<Bulle> BullesASupprimer { get => _bullesASupprimer; set => _bullesASupprimer = value; }
        internal List<Bulle> BullesAGonfler { get => _bullesAGonfler; set => _bullesAGonfler = value; }
        public Timer Rafraichir { get => _rafraichir; set => _rafraichir = value; }

        #endregion

        #region Méthodes

        #region Consctructeur
        public SceneParDefaut(Form vue)
        {
            // Récupération de la vue (FrmPrincipale)
            this.Vue = vue;

            // Initialisation du rafraichissement (timer)
            this.Rafraichir = new Timer();
            // Chaine de l'évènement de rafraichissement (timer)
            this.Rafraichir.Tick += Refresh_Tick;
            this.Rafraichir.Interval = 40;
            this.Rafraichir.Start();
            this.Vue.KeyPress += Vue_KeyPress;

            // Initialisation du minuteur (timer)
            this.Minuterie = new Timer();
            // Chaine de l'évènement de la minuterie (timer)
            this.Minuterie.Tick += Minuterie_Tick;
            this.Minuterie.Interval = 100;
            this.Minuterie.Start();

            // Chainer l'évènement du click sur la vue
            this.Vue.MouseClick += Vue_MouseClick;
            // Initialisation des variables de classe
            this.HauteurAquarium = HAUTEUR_AQUARIUM;
            this.LargeurAquarium = LARGEUR_AQUARIUM;
            this.Vue.Height = HAUTEUR_AQUARIUM;
            this.Vue.Width = LARGEUR_AQUARIUM;

            this.Vue.Resize += Vue_Resize;

            this.Rnd = new Random();

            // Initialisation des listes
            this.Bulles = new List<Bulle>();
            this.BullesASupprimer = new List<Bulle>();
            this.BullesAGonfler = new List<Bulle>();
            this.Poissons = new List<Poisson>();
        }

        private void Vue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
                changer = !changer;
            }
        }

        private void Vue_Resize(object sender, EventArgs e)
        {
            this.HauteurAquarium = this.Vue.Height;
            this.LargeurAquarium = this.Vue.Width;
            List<Poisson> poissonsASupprimer = new List<Poisson>();
            Poissons.ForEach(p =>
            {
                if (p.Position.Y > this.Vue.Height || p.Position.X > this.Vue.Width)
                {
                    poissonsASupprimer.Add(p);
                    this.Vue.Paint -= p.DessinerPoissonDepuisFonction;
                }
            });
            poissonsASupprimer.ForEach(p => this.Poissons.Remove(p));
            poissonsASupprimer.Clear();
        }
        #endregion

        private void Minuterie_Tick(object sender, EventArgs e)
        {
            // Pour chaque bulle de la liste, "b" représentant une bulle

            Bulle maBulle = new Bulle(
          new PointF(this.Rnd.Next(0, this.LargeurAquarium), this.Rnd.Next(this.HauteurAquarium - 100, this.HauteurAquarium)),
          new PointF(this.Rnd.Next(0, this.LargeurAquarium), -10));
            this.Bulles.Add(maBulle);

            Bulles.ForEach(b =>
            {
                b.Change = changer;
            });

            this.Vue.Paint += maBulle.Paint;

            Bulles.ForEach(b =>
            {
                if (b.estArrive)
                {
                    this.Vue.Paint -= b.Paint;
                }
            });
            Bulles.RemoveAll(b => b.estArrive);

            Poissons.ForEach(p =>
            {
                p.Change = changer;
                if (p.estArrive)
                {
                    p.ChangerDeSens();
                    p.InverserDirection();
                }
            });
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            this.Vue.Invalidate();
        }

        private void Vue_MouseClick(object sender, MouseEventArgs e)
        {
            Poisson monPoisson = new Poisson(e.Location, new PointF(50, e.Location.Y), 50, 50, 2500);
            this.Poissons.Add(monPoisson);
            this.Vue.Paint += monPoisson.DessinerPoissonDepuisFonction;
        }

        #region Bulles 
        /// <summary>
        /// Détéction des collisions entre les bulles
        /// </summary>
        /// <param name="bulle1">Objet bulle à tester pour les collisions</param>
        /// <returns>Booléen représentant si une collision est détectée</returns>
        public bool DetecteCollision(Bulle bulle1)
        {
            bool collision = false;

            this.Bulles.ForEach(bulle2 =>
            {
                if ((bulle1 != bulle2) && (!bulle1.Explose) && (!bulle2.Explose) &&
                (bulle1.BoiteDeCollision.IntersectsWith(bulle2.BoiteDeCollision)))
                {
                    this.BullesAGonfler.Add(bulle1);
                    bulle2.Explose = true;
                    this.BullesASupprimer.Add(bulle2);
                    collision = true;
                    return;
                }
            });
            return collision;
        }
        #endregion
        #endregion
    }
}
