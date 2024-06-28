using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs;

namespace Win_Design.Windows
{
    public partial class Project_Setting_Window : Form
    {
        public Project_Setting_Window()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Global_Variables.Project_Config.Project_C_Command_Window = checkBox1.Checked;
            Close();
        }

        private void Project_Setting_Window_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Global_Variables.Project_Config.Project_C_Command_Window;
        }
    }
}
