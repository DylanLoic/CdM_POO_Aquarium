using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    class Bulle : FormeAnimee
    {
        private const double RATIO_GRANDISSEMENT = 1.02;
        private const double RATIO_ACCELERATION = 0.85;

        public Bulle(PointF pDebut, PointF pFin, double largeur, double hauteur, double vitesse)
            : base(pDebut, pFin, largeur, hauteur, vitesse)
        {

        }
        public Bulle(PointF pDebut, PointF pFin) 
            : base(pDebut, pFin)
        {

        }

        public Bulle Fusionner(Bulle b)
        {
            double rayonOrigine = this.Largeur / 2;
            double rayonCible = b.Largeur / 2;

            double aireResultante = Math.Pow((Math.PI * rayonOrigine), 2) + Math.Pow((Math.PI * rayonCible), 2);

            double rayonResultant = Math.Sqrt(aireResultante / Math.PI);

            return new Bulle(this.Position, this.Fin, rayonResultant, rayonResultant, this.Duree);
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Red)), this.BoiteDeCollision);
        }
    }
}
