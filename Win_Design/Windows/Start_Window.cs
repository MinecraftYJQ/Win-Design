using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs;

namespace Win_Design.Windows
{
    public partial class Start_Window : Form
    {
        public Start_Window()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Project_Window add_Project_Window = new Add_Project_Window();
            add_Project_Window.ShowDialog();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string back_text = Open_Project.Open_Project_Window();
            if (back_text != "not file")
            {
                Global_Variables.Open_Project_Open_Path = back_text;
                Global_Variables.Open_Project_Path = Path.GetDirectoryName(Global_Variables.Open_Project_Open_Path);
                Global_Variables.Open_Project_Open_Design = true;
                Console.WriteLine(Global_Variables.Open_Project_Path);
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText($"{Global_Variables.Open_Project_Path}\\Main.Config").ToString());

                // 获取Command_Window的值
                string commandWindowValue = (string)jsonObject["Command_Window"];
                if (commandWindowValue == "True")
                {
                    Global_Variables.Project_Config.Project_C_Command_Window = true;
                }
                else
                {
                    Global_Variables.Project_Config.Project_C_Command_Window = false;
                }
                Close();
            }
        }

        private void Start_Window_Load(object sender, EventArgs e)
        {

        }

        private void Start_Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Global_Variables.Open_Project_Open_Design)
            {
                try
                {
                    Environment.Exit(0);
                }catch (Exception ex)
                {
                    Console.WriteLine("[Error]"+ex.Message);
                    //Thread.Sleep(1000);
                }
            }
        }
    }
}
