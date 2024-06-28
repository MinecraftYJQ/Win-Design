using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs.Controls;

namespace Win_Design.Windows
{
    public partial class Setting_Add_Control : Form
    {
        Design_Main_Window Designs;
        public Setting_Add_Control(Design_Main_Window windows)
        {
            InitializeComponent();
            Designs = windows;
        }

        private void Setting_Add_Control_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "标签":
                    Label Static = new Label();
                    Static.Name = "Static|0";
                    Static.Text = textBox5.Text;
                    Static.Location = new Point(int.Parse(textBox2.Text), int.Parse(textBox1.Text));
                    Static.Width = int.Parse(textBox4.Text);
                    Static.Height = int.Parse(textBox3.Text);
                    Static.Click += new System.EventHandler(Cick);
                    Designs.add_con(Static);
                    break;
                case "按钮":
                    Button button = new Button();
                    button.Name = "Button|0";
                    button.Text = textBox5.Text;
                    button.Location = new Point(int.Parse(textBox2.Text), int.Parse(textBox1.Text));
                    button.Width = int.Parse(textBox4.Text);
                    button.Height = int.Parse(textBox3.Text);
                    button.Click += new System.EventHandler(Cick);
                    Designs.add_con(button);
                    break;
                case "输入框":
                    TextBox Edit = new TextBox();
                    Edit.Name = "Edit|0";
                    Edit.Text = textBox5.Text;
                    Edit.Location = new Point(int.Parse(textBox2.Text), int.Parse(textBox1.Text));
                    Edit.Width = int.Parse(textBox4.Text);
                    Edit.Height = int.Parse(textBox3.Text);
                    Edit.Click += new System.EventHandler(Cick);
                    Designs.add_con(Edit);
                    break;
            }

            Close();
        }
        private void Cick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Control_Actions.Del_Window(control);
        }
    }
}
