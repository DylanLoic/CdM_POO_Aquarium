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
            DoubleBuffered = true;
            Aquarium aquarium = new Aquarium(this);
        }
    }
}
