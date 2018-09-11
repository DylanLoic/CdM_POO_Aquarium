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
        protected Point debut;
        protected Point fin;
        // Variable de type Entier, contient un chiffre positif ou négatif sans virgule
        protected int largeur;
        // Variable de type Double, contient un chiffre positif ou négatif avec virgule
        protected double duree;
        // Variable de type "Chronomètre", permet d'effectuer des mesures de temps
        protected Stopwatch chrono;
        // Variable de type "Aléatoire" retourne un chiffre aléatoire dans une plage donnée
        protected Random rnd;
        #endregion

        #region Propriétés
        /// <summary>
        /// Calcule la position en fonction du temps écoulé depuis la création de l'objet
        /// Retourne la position au moment du calcul
        /// </summary>
        public Point Position
        {
            get
            {
                double Y = debut.Y + (double)(fin.Y - debut.Y) * (double)(chrono.ElapsedMilliseconds / duree);
                double X = debut.X + (double)(fin.X - debut.X) * (double)(chrono.ElapsedMilliseconds / duree);
                if (estArrive)
                {
                    chrono.Stop();
                    Y = fin.Y;
                    X = fin.X;
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
                return chrono.ElapsedMilliseconds >= duree;
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
        public FormeAnimee(int x0, int y0, int xE, int yE, int largeur, double vitesse)
        {
            debut = new Point(x0, y0);
            this.largeur = largeur;
            duree = vitesse;
            fin = new Point(xE, yE);
            chrono = new Stopwatch();
            rnd = new Random();
            chrono.Start();

        }

        public FormeAnimee(Point pDebut, Point pFin, int largeur, double vitesse)
            : this(pDebut.X, pDebut.Y, pFin.X, pFin.Y, largeur, vitesse)
        {
        }

        public FormeAnimee(Point pDebut, Point pFin)
            : this(pDebut, pFin, 50, 1000)
        {
        }

        public FormeAnimee() : this(new Point(100, 100), new Point(500, 100))
        {
        }

        #endregion

        public void InverserDirection()
        {
            Point temp;
            temp = this.debut;
            this.debut = this.fin;
            this.fin = temp;
            chrono.Restart();
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
        public Carre():base()
        {
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(Position, new Size(largeur, largeur)));
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
