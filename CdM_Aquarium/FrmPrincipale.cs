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
        //Déclaration des variables
        // Variable de type Timer, permet d'effectuer des tâches à un intervalle précis
        Timer t;
        // Variable de type "Liste" et "FormeAnimee" elle permet de contenir plusieurs variables de type "FormeAnimee"
        List<FormeAnimee> listeFormes;

        // Initialisation de la fenêtre principale de l'application
        public frmPrincipale()
        {
            DoubleBuffered = true;
            listeFormes = new List<FormeAnimee>();
            t = new Timer();
            t.Interval = 10;
            t.Tick += T_Tick;
            t.Start();
            InitializeComponent();
            listeFormes.Add(new Carre());    // Ajout d'objets dans la liste
            listeFormes.Add(new Poisson());  // ""
            listeFormes.ForEach(p => this.Paint += p.Paint);
                
        }
        

        private void T_Tick(object sender, EventArgs e)
        {
           Invalidate();
            foreach (var objet in listeFormes)
            {
                if (objet.estArrive)
                    objet.InverserDirection();
            }
        }
    }
}
