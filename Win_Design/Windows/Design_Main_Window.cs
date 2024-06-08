using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Design.Windows
{
    public partial class Design_Main_Window : Form
    {
        public Design_Main_Window()
        {
            InitializeComponent();
        }

        public int Get_hWnd()
        {
            return (int)this.Handle;
        }
        private void Design_Main_Window_Load(object sender, EventArgs e)
        {

        }
    }
}
