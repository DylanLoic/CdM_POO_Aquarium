using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    class Aquarium
    {
        #region Champs
        private Form _vue;
        private Timer _minuterie;
        private List<Bulle> _bulles;
        private Random _rnd;
        private int _hauteurAquarium;
        private int _largeurAquarium;
<<<<<<< HEAD
        private List<Bulle> bullesASupprimer;
        private List<FormeAnimee> _formesAnimees;


=======
        private List<Bulle> _bullesASupprimer;
>>>>>>> 6862878a7c107a7cb02f9325a8cb3c429dbd3ce6
        #endregion

        #region Propriétés
        /// <summary>
        /// Liste des bulles de l'aquarium
        /// </summary>
        private List<Bulle> Bulles { get => _bulles; set => _bulles = value; }
        private List<FormeAnimee> FormesAnimees { get => _formesAnimees; set => _formesAnimees = value; }
        public int HauteurAquarium { get => _hauteurAquarium; set => _hauteurAquarium = value; }
        public int LargeurAquarium { get => _largeurAquarium; set => _largeurAquarium = value; }
        public Form Vue { get => _vue; set => _vue = value; }
        public Timer Minuterie { get => _minuterie; set => _minuterie = value; }
        public Random Rnd { get => _rnd; set => _rnd = value; }

        /// <summary>
        /// Liste stockant les bulles à supprimer
        /// </summary>
        public List<Bulle> BullesASupprimer { get => _bullesASupprimer; set => _bullesASupprimer = value; }
        #endregion

        #region Méthodes

        #region Consctructeur
        public Aquarium(Form vue)
        {
            // Récupération de la vue (FrmPrincipale)
            this.Vue = vue;

            // Initialisation du minuteur (timer)
            this.Minuterie = new Timer();
            this.Minuterie.Interval = 10;
            this.Minuterie.Start();

            // Chainer de l'évènement de la minuterie (timer)
            this.Minuterie.Tick += Minuterie_Tick;

            // Chainer l'évènement du click sur la vue
            this.Vue.MouseClick += Vue_MouseClick;

            // Initialisation des variables de classe
            this.HauteurAquarium = this.Vue.Height;
            this.LargeurAquarium = this.Vue.Width;
            this.Rnd = new Random();
            Bulles = new List<Bulle>();
            bullesASupprimer = new List<Bulle>();
            FormesAnimees = new List<FormeAnimee>();
        }
        #endregion

        private void Minuterie_Tick(object sender, EventArgs e)
        {
            this.Vue.Invalidate();
            bool collision = false;

            Bulles.ForEach(b =>
            {
                collision = DetecteCollision(b);
                if (collision)
                    return;
            });

            if (collision)
                FusionBulle();
 
            Bulles.ForEach(b =>
            {
                if (objet.estArrive)
                    objet.InverserDirection();
            }

            foreach (FormeAnimee f in FormesAnimees)
            {
                if (f is Poisson)
                {
                    if (f.estArrive)
                    {
                        f.InverserDirection();
                    }
                }
            }

        }

        private void Vue_MouseClick(object sender, MouseEventArgs e)
        {
            //Bulle maBulle = new Bulle(e.Location, new System.Drawing.PointF(0, 0));
            //Bulles.Add(maBulle);
            //this.Vue.Paint += maBulle.Paint;
            Poisson monPoisson = new Poisson(e.Location, new PointF(50,50));
            FormesAnimees.Add(monPoisson);
            this.Vue.Paint += monPoisson.DessinerPoissonDepuisFonction;
            
            Bulle maBulle = new Bulle(e.Location, new PointF(0, 0));
            this.Bulles.Add(maBulle);
            this.Vue.Paint += maBulle.Paint;
        }

        #region Bulles 
        public bool DetecteCollision(Bulle bulle1)
        {
            bool collision = false;

            this.Bulles.ForEach(bulle2 =>
            {
                if ((bulle1 != bulle2) &&
                (bulle2.BoiteDeCollision.IntersectsWith(bulle1.BoiteDeCollision)))
                {
                    this.BullesASupprimer.Add(bulle1);
                    this.BullesASupprimer.Add(bulle2);
                    collision = true;
                    return;
                }
            });
            return collision;
        }

        private void FusionBulle()
        {
            Bulle bulleAAjouter = this.BullesASupprimer[0].Fusionner(BullesASupprimer[1]);

            // Retire du Paint de la vue les bulles qui vont être supprimées
            this.BullesASupprimer.ForEach(p => this.Vue.Paint -= p.Paint);
            // Supprime les bulles de la liste principale
            this.BullesASupprimer.ForEach(p => Bulles.Remove(p));
            this.BullesASupprimer.Clear();

            // Ajoute à la liste la nouvelle bulle créée de la fusion
            this.Bulles.Add(bulleAAjouter);
            // Ajout au Paint de la vue la nouvelle bulle
            this.Vue.Paint += bulleAAjouter.Paint;
        }
        #endregion

        #endregion
    }
}
