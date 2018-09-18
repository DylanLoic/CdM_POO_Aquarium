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
        private Point _debut;
        private Point _fin;
        // Variable de type Entier, contient un chiffre positif sans virgule
        private int _largeur;
        // Variable de type Entier, contient un chiffre positif sans virgule
        private int _hauteur;
        // Variable de type Double, contient un chiffre positif avec virgule
        private double _duree;
        // Variable de type "Chronomètre", permet d'effectuer des mesures de temps
        private Stopwatch _chrono;
        // Variable de type "Aléatoire" retourne un chiffre aléatoire dans une plage donnée
        private Random _rnd;

        private Rectangle _boiteDeCollision;

        #endregion

        #region Propriétés

        public Point Debut { get => _debut; set => _debut = value; }
        public Point Fin { get => _fin; set => _fin = value; }
        public int Largeur { get => _largeur; set => _largeur = value; }
        public int Hauteur { get => _hauteur; set => _hauteur = value; }
        public double Duree { get => _duree; set => _duree = value; }
        public Stopwatch Chrono { get => _chrono; set => _chrono = value; }
        public Random Rnd { get => _rnd; set => _rnd = value; }
        public Rectangle BoiteDeCollision { get => _boiteDeCollision; set => _boiteDeCollision = value; }

        /// <summary>
        /// Calcule la position en fonction du temps écoulé depuis la création de l'objet
        /// Retourne la position au moment du calcul
        /// </summary>
        public Point Position
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
        public FormeAnimee(int x0, int y0, int xE, int yE, int largeur, int hauteur, double vitesse)
        {
            this.Debut = new Point(x0, y0);
            this.Fin = new Point(xE, yE);
            this.Largeur = largeur;
            this.Hauteur = hauteur;
            this.Duree = vitesse;

            this.Chrono = new Stopwatch();
            this.Rnd = new Random();
            this.Chrono.Start();

        }

        public FormeAnimee(Point pDebut, Point pFin, int largeur, int hauteur, double vitesse)
            : this(pDebut.X, pDebut.Y, pFin.X, pFin.Y, largeur, hauteur, vitesse)
        {
        }

        public FormeAnimee(Point pDebut, Point pFin)
            : this(pDebut, pFin, 50, 50, 1000)
        {
        }

        public FormeAnimee() : this(new Point(100, 100), new Point(500, 100))
        {
        }

        #endregion

        public void InverserDirection()
        {
            Point temp;
            temp = this.Debut;
            this.Debut = this.Fin;
            this.Fin = temp;
            this.Chrono.Restart();
        }

        /// <summary>
        /// Méthodes Dessiner en abstraite
        /// </summary>
        abstract public void Paint(object sender, PaintEventArgs e);
        /*{
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(Position, new Size(largeur, largeur)));
        }*/

        #endregion
    }

    class Carre : FormeAnimee
    {
        public Carre() : base()
        {
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(Position, new Size(this.Largeur, this.Hauteur)));
        }
    }

    class Poisson : FormeAnimee
    {
        public Poisson() : base()
        {

        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.Red)), new RectangleF(Position, new SizeF(120, 60)));
        }
    }

}
