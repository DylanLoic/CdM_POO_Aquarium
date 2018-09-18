using System;
using System.Collections.Generic;
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
        }

        private void Minuterie_Tick(object sender, EventArgs e)
        {
            this.Vue.Invalidate();
            foreach (var objet in Bulles)
            {
                if (objet.estArrive)
                    objet.InverserDirection();
            }
        }

        private void Vue_MouseClick(object sender, MouseEventArgs e)
        {
            Bulle maBulle = new Bulle(e.Location, new System.Drawing.PointF(800, 800));
            this.Vue.Paint += maBulle.Paint;
            MessageBox.Show(maBulle.Hauteur.ToString());

            Bulles.Add(maBulle);
        }
        #endregion

        #region Bulles 
        private void GenerationBulles(int nb)
        {

        }


        public void ParcourirBulles()
        {
            for (int i = 0; i < Bulles.Count; i++)
            {
                for (int j = i + 1; j < Bulles.Count; j++)
                {
                    if (VerifieCollision(Bulles[i], Bulles[j]))
                    {
                        FusionBulle(Bulles[i], Bulles[j]);
                        return;
                    }
                }
            }
        }

        private void FusionBulle(Bulle b1, Bulle b2)
        {
            Bulle nouvelleBulle = b1.Fusionner(b2);
            this.Vue.Paint -= b1.Paint;
            this.Vue.Paint -= b2.Paint;
            Bulles.Remove(b1);
            Bulles.Remove(b2);
            this.Vue.Paint += nouvelleBulle.Paint;

        }

        private bool VerifieCollision(Bulle b1, Bulle b2)
        {
            return b1.BoiteDeCollision.IntersectsWith(b2.BoiteDeCollision);
        }
        #endregion

        #endregion

    }
}
