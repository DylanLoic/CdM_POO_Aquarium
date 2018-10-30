using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace CdM_Aquarium
{
    abstract class FormeAnimee
    {
        #region Variables d'instance
        // Variable de type Point, contient une position X et une position Y
        // Variable d'instance permentant d'avoir la position de début de la forme animée
        private PointF _debut;
        // Variable de type Point, contient une position X et une position Y
        // Variable d'instance permentant d'avoir la position de fin de la forme animée
        private PointF _fin;
        // Variable de type Entier, contient un chiffre positif sans virgule
        // Variable permettant d'avoir la largeur de la forme animée
        private double _largeur;
        // Variable de type Entier, contient un chiffre positif sans virgule
        // Variable permettant d'avoir la hauteur de la forme animée
        private double _hauteur;
        // Variable de type Double, contient un chiffre positif avec virgule
        // Variable permettant d'avoir la durée du déplacement entre le point de début et le point de fin
        private double _duree;
        // Variable de type "Chronomètre", permet d'effectuer des mesures de temps
        private Stopwatch _chrono;
        // Variable Rectangle englobant la forme animée
        private RectangleF _boiteDeCollision;

        #endregion

        #region Propriétés
        public PointF Debut { get => _debut; private set => _debut = value; }
        public PointF Fin { get => _fin; private set => _fin = value; }
        public double Largeur { get => _largeur; private set => _largeur = value; }
        public double Hauteur { get => _hauteur; private set => _hauteur = value; }
        public double Duree { get => _duree; private set => _duree = value; }
        public Stopwatch Chrono { get => _chrono; private set => _chrono = value; }
        public RectangleF BoiteDeCollision
        {
            get
            {
                return new RectangleF(this.Position, new SizeF((float)this.Largeur, (float)this.Hauteur));
            }
            private set => _boiteDeCollision = value;
        }

        /// <summary>
        /// Calcule la position en fonction du temps écoulé depuis la création de l'objet
        /// Retourne la position au moment du calcul
        /// </summary>
        public PointF Position
        {
            get
            {
                double Y = Debut.Y + (double)(Fin.Y - Debut.Y) * (double)(Chrono.ElapsedMilliseconds / Duree);
                double X = Debut.X + (double)(Fin.X - Debut.X) * (double)(Chrono.ElapsedMilliseconds / Duree);
                if (estArrive)
                {
                    Chrono.Stop();
                    Y = Fin.Y;
                    X = Fin.X;
                }
                return new Point((int)X, (int)Y);
            }
        }

        /// <summary>
        /// Vérifie si le temps écoulé est égal à la durée définie
        /// </summary>
        public bool estArrive
        {
            get
            {
                return Chrono.ElapsedMilliseconds >= Duree;
            }
        }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur dédié
        /// </summary>
        /// <param name="x0">X Début </param>
        /// <param name="y0">Y Début </param>
        /// <param name="xE">X Fin </param>
        /// <param name="yE">Y Fin </param>
        /// <param name="largeur">Taille de la forme </param>
        /// <param name="vitesse">Vitesse (représentée par une durée) </param>
        public FormeAnimee(double x0, double y0, double xE, double yE, double largeur, double hauteur, double vitesse)
        {
            this.Debut = new PointF((float)x0, (float)y0);
            this.Fin = new PointF((float)xE, (float)yE);
            this.Largeur = largeur;
            this.Hauteur = hauteur;
            this.Duree = vitesse;

            this.BoiteDeCollision = new RectangleF(this.Debut, new SizeF((float)this.Largeur, (float)this.Hauteur));
            this.Chrono = new Stopwatch();

            this.Chrono.Start();
        }

        public FormeAnimee(PointF pDebut, PointF pFin, double largeur, double hauteur, double vitesse)
            : this(pDebut.X, pDebut.Y, pFin.X, pFin.Y, largeur, hauteur, vitesse)
        {
        }

        public FormeAnimee(PointF pDebut, PointF pFin)
            : this(pDebut, pFin, 50, 50, 1000)
        {
        }

        public FormeAnimee() : this(new PointF(100, 100), new PointF(500, 100))
        {
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Inverse la direction de l'objet se déplacant
        /// </summary>
        public void InverserDirection()
        {
            PointF temp;
            temp = this.Debut;
            this.Debut = this.Fin;
            this.Fin = temp;
            this.Chrono.Restart();
        }

        /// <summary>
        /// Permet de faire grandir la forme animée.
        /// </summary>
        /// <param name="pLargeur">Ajoute la valeur à la largeur de l'objet</param>
        /// <param name="pHauteur">Ajoute la valeur à la hauteur de l'objet</param>
        public void GrandirForme(double pLargeur, double pHauteur)
        {
            this.Largeur += pLargeur;
            this.Hauteur += pHauteur;
        }

        /// <summary>
        /// Méthodes Paint en abstraite affin de pouvoir la surchargée lors des différents héritages
        /// </summary>
        abstract public void Paint(object sender, PaintEventArgs e);
        #endregion
    }

    // Exemple d'héritage de FormeAnimee et d'override de la méthode Paint
    //class Carre : FormeAnimee
    //{
    //    public Carre() : base()
    //    {
    //    }

    //    public override void Paint(object sender, PaintEventArgs e)
    //    {
    //        e.Graphics.DrawRectangle(Pens.Blue, Position.X, Position.Y, this.BoiteDeCollision.Width, this.BoiteDeCollision.Height);
    //    }
    //}
}
