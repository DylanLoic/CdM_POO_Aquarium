/*
  * Auteurs : Schito Dylan, Périsset Kilian, Brunazzi Robin
  * Date : 18.09.2018
  * Projet : Cité des métiers
  * Description : 
  */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CdM_Aquarium
{
    public partial class frmPrincipale : Form
    {
        public frmPrincipale()
        {
            this.Icon = Properties.Resources.icon_poisson;
            DoubleBuffered = true;
            //Aquarium aquarium = new Aquarium(this);
            SceneParDefaut scene1 = new SceneParDefaut(this);

        }
    }
}
