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
        // Variable permettant d'acceder à la couleur de la bulle
        private Color _color;
        // Variable permettant de changer l'état de la bulle.
        private bool _explose;
        private bool _change;
        #endregion

        #region Propriétés
        public Color Color { get => _color; set => _color = value; }
        public bool Explose { get => _explose; private set => _explose = value; }
        public bool Change { get => _change; set => _change = value; }

        #endregion

        #region Constructeurs
        public Bulle(PointF pDebut, PointF pFin, double largeur, double hauteur, double vitesse)
            : base(pDebut, pFin, largeur, hauteur, vitesse)
        {
            this.Change = false;
            this.Explose = false;
        }
        public Bulle(PointF pDebut, PointF pFin)
            : base(pDebut, pFin, 10, 10, 3000)
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
            this.GrandirForme(5,5);
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Red, 4);

            if (!this.Change)
                e.Graphics.FillEllipse(new SolidBrush(Color.LightBlue), this.BoiteDeCollision);
            else
                e.Graphics.DrawEllipse(myPen, this.BoiteDeCollision);
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
