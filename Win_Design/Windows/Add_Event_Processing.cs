using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs.Make_Cpp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Win_Design.Windows
{
    public partial class Add_Event_Processing : Form
    {
        Control controls;
        public Add_Event_Processing(Control control)
        {
            InitializeComponent();
            controls = control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(richTextBox1.Text);
            Make_Event_Processing_Json.Write_Event_Processing_Json(Make_Event_Processing_Json.Make_Event_Processing_Json_Out(controls, richTextBox1.Text));
            Close();
        }

        private void Add_Event_Processing_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Make_Event_Processing_Json.Get_Event_Processing(controls);
        }
    }
}
