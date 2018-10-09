/*
 * Auteur : Dylan Schito, Kilian Perisset
 * Date : 02.10.2018
 * Projet : Cité des métiers
 * Description :
 */

using System.Drawing;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    class Bulle : FormeAnimee
    {
        #region Champs
        private Color _color;
        private bool _explose;
        #endregion

        #region Propriétés
        public Color Color { get => _color; set => _color = value; }
        public bool Explose { get => _explose; set => _explose = value; }

        #endregion

        #region Constructeurs
        public Bulle(PointF pDebut, PointF pFin, double largeur, double hauteur, double vitesse)
            : base(pDebut, pFin, largeur, hauteur, vitesse)
        {
            this.Explose = false;
        }
        public Bulle(PointF pDebut, PointF pFin)
            : base(pDebut, pFin, 10, 10, 2000)
        {
        }

        public Bulle()
            : this(new PointF(100, 0), new PointF(100, 100))
        {
        }
        #endregion

        #region Méthodes

       
        public void Gonfler()
        {
            this.Largeur += 5;
            this.Hauteur += 5;
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), this.BoiteDeCollision.X, this.BoiteDeCollision.Y, this.BoiteDeCollision.Width, this.BoiteDeCollision.Height);
            //e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Red)), this.Position.X, this.Position.Y, (float)this.Largeur, (float)this.Hauteur);
            e.Graphics.FillEllipse(new SolidBrush(Color.LightBlue), this.BoiteDeCollision);
        }
        #endregion
        //public Bulle Fusionner(Bulle b)
        //{
        //    double rayonOrigine = this.Largeur / 2;
        //    double rayonCible = b.Largeur / 2;

        //    double aireResultante = Math.Pow((Math.PI * rayonOrigine), 2) + Math.Pow((Math.PI * rayonCible), 2);

        //    double rayonResultant = Math.Sqrt(aireResultante / Math.PI);

        //    return new Bulle(this.Position, this.Fin, rayonResultant, rayonResultant, this.Duree);
        //}
    }
}
