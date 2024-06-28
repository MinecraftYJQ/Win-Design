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
    public partial class Add_Project_Window : Form
    {
        public Add_Project_Window()
        {
            InitializeComponent();
        }

        string paths= Path_API.Get_Desktop_Path()+"\\";
        string name = "New Project";


        private void Add_Project_Window_Load(object sender, EventArgs e)
        {
            Path.Text= paths;
            Location = new Point(Location.X, Location.Y - 100);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name= textBox1.Text;
        }

        private void Path_TextChanged(object sender, EventArgs e)
        {
            paths = Path.Text;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if(Add_Project.Add_Project_Main(paths, name))
            {
                MessageBox.Show("项目创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Global_Variables.Open_Project_Open_Path = paths+name+$"\\{name}.w32project";
                Global_Variables.Open_Project_Path = System.IO.Path.GetDirectoryName(Global_Variables.Open_Project_Open_Path);
                Global_Variables.Open_Project_Open_Design = true;
                Close();
            }
            else
            {
                MessageBox.Show("项目创建失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Add_Project_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Global_Variables.Open_Project_Open_Design)
            {
                Global_Variables.Open_Project_Open_Design = false;
            }
        }
    }
}
