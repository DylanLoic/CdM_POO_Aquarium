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
        private List<Bulle> bullesASupprimer;


        #endregion

        #region Propriétés
        private List<Bulle> Bulles { get => _bulles; set => _bulles = value; }
        public int HauteurAquarium { get => _hauteurAquarium; set => _hauteurAquarium = value; }
        public int LargeurAquarium { get => _largeurAquarium; set => _largeurAquarium = value; }
        public Form Vue { get => _vue; set => _vue = value; }
        public Timer Minuterie { get => _minuterie; set => _minuterie = value; }
        public Random Rnd { get => _rnd; set => _rnd = value; }
        #endregion

        #region Méthodes
        #region Consctructeur
        public Aquarium(Form vue)
        {
            this.Vue = vue;

            this.Minuterie = new Timer();
            this.Minuterie.Interval = 10;
            this.Minuterie.Start();
            this.Minuterie.Tick += Minuterie_Tick;
            this.Vue.MouseClick += Vue_MouseClick;
            this.HauteurAquarium = this.Vue.Height;
            this.LargeurAquarium = this.Vue.Width;
            this.Rnd = new Random();
            Bulles = new List<Bulle>();
            bullesASupprimer = new List<Bulle>();
        }

        private void Minuterie_Tick(object sender, EventArgs e)
        {
            this.Vue.Invalidate();
            bool temp = false;
            foreach (Bulle b in Bulles)
            {
                if (DetecteCollision(b))
                {
                    b.Color = Color.Blue;
                    temp = true;
                }
            }
            if (temp)
                FusionBulle();

            foreach (var objet in Bulles)
            {
                if (objet.estArrive)
                    objet.InverserDirection();
            }
        }

        private void Vue_MouseClick(object sender, MouseEventArgs e)
        {
            Bulle maBulle = new Bulle(e.Location, new System.Drawing.PointF(0, 0));
            Bulles.Add(maBulle);
            this.Vue.Paint += maBulle.Paint;
        }
        #endregion

        #region Bulles 

        public bool DetecteCollision(Bulle bulle1)
        {
            bool collision = false;
            foreach (Bulle bulle2 in Bulles)
                if ((bulle1 != bulle2) &&
                    (bulle2.BoiteDeCollision.IntersectsWith(bulle1.BoiteDeCollision)))
                {
                    bullesASupprimer.Add(bulle1);
                    bullesASupprimer.Add(bulle2);
                    collision = true;
                    break;
                }
            return collision;
        }

        private void FusionBulle(/*Bulle b1, Bulle b2*/)
        {
            //Bulle nouvelleBulle = b1.Fusionner(b2);
            //this.Vue.Paint -= b1.Paint;
            //this.Vue.Paint -= b2.Paint;
            //Bulles.Remove(b1);
            //Bulles.Remove(b2);
            //Bulles.Add(nouvelleBulle);
            //this.Vue.Paint += nouvelleBulle.Paint;


            Bulle bulleAAjouter = bullesASupprimer[0].Fusionner(bullesASupprimer[1]);
            bullesASupprimer.ForEach(p => this.Vue.Paint -= p.Paint);
            bullesASupprimer.ForEach(p => Bulles.Remove(p));
            bullesASupprimer.Clear();
            Bulles.Add(bulleAAjouter);
            this.Vue.Paint += bulleAAjouter.Paint;
        }
        #endregion
        #endregion
    }
}
