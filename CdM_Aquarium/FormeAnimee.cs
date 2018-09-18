using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace CdM_Aquarium
{
    abstract class FormeAnimee
    {
        #region
        // Déclaration des variables
        // Variable de type Point, contient une position X et une position Y
        private PointF _debut;
        private PointF _fin;
        // Variable de type Entier, contient un chiffre positif sans virgule
        private double _largeur;
        // Variable de type Entier, contient un chiffre positif sans virgule
        private double _hauteur;
        // Variable de type Double, contient un chiffre positif avec virgule
        private double _duree;
        // Variable de type "Chronomètre", permet d'effectuer des mesures de temps
        private Stopwatch _chrono;

        private RectangleF _boiteDeCollision;

        #endregion

        #region Propriétés

        public PointF Debut { get => _debut; set => _debut = value; }
        public PointF Fin { get => _fin; set => _fin = value; }
        public double Largeur { get => _largeur; set => _largeur = value; }
        public double Hauteur { get => _hauteur; set => _hauteur = value; }
        public double Duree { get => _duree; set => _duree = value; }
        public Stopwatch Chrono { get => _chrono; set => _chrono = value; }
        public RectangleF BoiteDeCollision { get => _boiteDeCollision; set => _boiteDeCollision = value; }

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

        #region Méthodes
        #region Constructeurs
        /// <summary>
        /// Constructeur dédié
        /// </summary>
        /// <param name="x0">X Début </param>
        /// <param name="y0">Y Début </param>
        /// <param name="xE">X Fin </param>
        /// <param name="yE">Y Fin </param>
        /// <param name="largeur">Taille de la forme </param>
        /// <param name="vitesse">Vitesse </param>
        public FormeAnimee(double x0, double y0, double xE, double yE, double largeur, double hauteur, double vitesse)
        {
            this.Debut = new PointF((float)x0, (float)y0);
            this.Fin = new PointF((float)xE, (float)yE);
            this.Largeur = largeur;
            this.Hauteur = hauteur;
            this.Duree = vitesse;

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

        public void InverserDirection()
        {
            PointF temp;
            temp = this.Debut;
            this.Debut = this.Fin;
            this.Fin = temp;
            this.Chrono.Restart();
        }

        /// <summary>
        /// Méthodes Dessiner en abstraite
        /// </summary>
        abstract public void Paint(object sender, PaintEventArgs e);
        #endregion
    }

    class Carre : FormeAnimee
    {
        public Carre() : base()
        {
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, Position.X, Position.Y, (float)this.Largeur, (float)this.Hauteur);
        }
    }



}
