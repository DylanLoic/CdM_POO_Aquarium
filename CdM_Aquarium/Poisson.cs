/*
  * Auteurs : Brunazzi Robin, Périsset Killian 
  * Date : 18.09.2018
  * Projet : 
  * Description : 
  */
using System.Drawing;
using System.Windows.Forms;

namespace CdM_Aquarium
{
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
