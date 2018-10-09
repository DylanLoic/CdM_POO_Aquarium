﻿/*
  * Auteurs : Brunazzi Robin
  * Date : 18.09.2018
  * Projet : Cité des métiers
  * Description : 
  */

// HitBox
// this.BoiteDeCollision = new RectangleF(this.Position.X, this.Position.Y, (float)this.Hauteur, (float)this.Hauteur);

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CdM_Aquarium
{

    class Poisson : FormeAnimee
    {
        int _sensPoisson;

        public int SensPoisson { get => _sensPoisson; set => _sensPoisson = value; }

        #region Constructeurs
        /*public Poisson() :
            base()
        {
        }*/

        public Poisson(PointF pDebut, PointF pFin) :
            base(pDebut, pFin)
        {
            if (pDebut.X < pFin.X)
                this.SensPoisson = 1;
            else
                this.SensPoisson = -1;
        }

        public Poisson(PointF pDebut, PointF pFin, double pLargeur, double pHauteur, double pVitesse) :
            base(pDebut, pFin, pLargeur, pHauteur, pVitesse)
        {
            if (pDebut.X < pFin.X)
                this.SensPoisson = 1;
            else
                this.SensPoisson = -1;
        }
        #endregion

        #region Méthodes
        public override void Paint(object sender, PaintEventArgs e)
        {
        }

        //Déssine le poisson depuis une image stockée dans les ressources du projet Visual Studio
        private void DessinerPoissonDepuisImage(object sender, PaintEventArgs e)
        {
        }

        //Permet au poisson de se "tourner" vers l'autre direction
        public void ChangerDeSens()
        {
            this.SensPoisson *= -1;
        }

        //Inverser direction avec -x
        public PointF CourbePoisson(double t)
        {
            double x, y;
            x = (this.Hauteur * Math.Cos(t) - this.Hauteur * Math.Sin(t) * Math.Sin(t) / Math.Sqrt(2))*this.SensPoisson;
            y = (this.Hauteur * Math.Cos(t) * Math.Sin(t));
            return new PointF(Position.X + Convert.ToSingle(x), Position.Y + Convert.ToSingle(y));
        }

        //Déssine le poisson depuis une fonction paramétrique (fonction mathématique) 
        public void DessinerPoissonDepuisFonction(object sender, PaintEventArgs e)
        {
            //Permet un dessin plus "affiné"
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            double t = 0;
            double deltat = 2.0 * Math.PI / 100.0;
            //"Précision" à 100 lignes 0->99
            for (int i = 0; i < 99; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Red), CourbePoisson(t), CourbePoisson(t + deltat));
                t = t + deltat;
            }
        }
        #endregion
    }
}
