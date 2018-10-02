/*
  * Auteurs : Brunazzi Robin, Périsset Killian, Schito Dylan
  * Date : 18.09.2018
  * Projet : 
  * Description : 
  */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    
    class Poisson : FormeAnimee
    {
        //Taille du poisson à afficher

        public Poisson() : base()
        {
            
        }



        public Poisson(PointF pDebut, PointF pFin) : base(pDebut, pFin)
        {

        }

        public Poisson(PointF pDebut, PointF pFin, double largeur, double hauteur, double vitesse) : base(pDebut, pFin, largeur, hauteur, vitesse)
        {

        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Red)), new RectangleF(Position, new SizeF(120, 60)));
        }

        //DrawFromImage
        private void DessinerPoissonDepuisImage(object sender, PaintEventArgs e)
        {

        }


        //Inverser direction avec -x
        public PointF CourbePoisson(double t)
        {
            double x,y;
            x = (this.Hauteur * Math.Cos(t) - this.Hauteur * Math.Sin(t) * Math.Sin(t) / Math.Sqrt(2));
            y = (this.Hauteur * Math.Cos(t) * Math.Sin(t));
            return new PointF(Position.X + Convert.ToSingle(x), Position.Y+ Convert.ToSingle(y));
        }

        //DrawFromFunction
        public void DessinerPoissonDepuisFonction(object sender, PaintEventArgs e)
        {
            //Permet un dessin plus "affiné"
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            double t = 0;
            double deltat = 2.0 * Math.PI / 100.0;
            for (int i = 0; i < 99; i++) {
               e.Graphics.DrawLine(new Pen(Color.Blue), CourbePoisson(t), CourbePoisson(t+deltat));
                t = t + deltat;
            }
        }
    }
}
