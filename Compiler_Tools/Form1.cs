using Compiler_Tools.Cs;
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

namespace Compiler_Tools
{
    public partial class Form1 : Form
    {
        string open_file; string open_path; string name; bool command_window;
        public Form1(string open_file1,string open_path1,string name1,bool command_window1)
        {
            InitializeComponent();
            open_file = open_file1;
            open_path = open_path1;
            name = name1;
            command_window = command_window1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            Task.Run(() =>
            {
                while (true)
                {
                    Text = "编译工具 作者：B站 Minecraft一角钱";
                    Thread.Sleep(1000);
                }
            });
            Directory.CreateDirectory("C:\\Win-Design");
            File.WriteAllBytes("C:\\Win-Design\\MinGW-Install.exe", global::Compiler_Tools.Properties.Resources.mingw_get_setup);
            textBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            int temp = 0;
            if (open_file != "")
            {
                textBox1.Text = open_file;
                temp++;
            }
            if (open_path != "")
            {
                textBox2.Text = open_path;
                temp++;
            }
            if (name != "")
            {
                textBox3.Text=name;
                temp++;
            }
            checkBox1.Checked = !command_window;
            if (temp == 3)
            {
                Start_Command.Start_Command_Go($"taskkill /f /im {textBox3.Text}");
                string canshu = "";
                if (checkBox1.Checked)
                {
                    canshu += " -mwindows";
                }
                string command = $"g++ \"{textBox1.Text}\" -o \"{textBox2.Text}\\{textBox3.Text}\"{canshu}";
                Console.WriteLine(command);
                Start_Command.Start_Command_Go(command);
                MessageBox.Show("编译成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string main_path = Open_Path.Get_Open_File_Path();
            if (main_path != "not file")
            {
                textBox1.Text = main_path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string out_path = Open_Path.Get_Porject_Out_Path();
            if (out_path != "not file")
            {
                textBox2.Text = out_path;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start_Command.Start_Command_Go($"taskkill /f /im {textBox3.Text}");
            string canshu = "";
            if (checkBox1.Checked)
            {
                canshu += " -mwindows";
            }
            string command = $"g++ \"{textBox1.Text}\" -o \"{textBox2.Text}\\{textBox3.Text}\"{canshu}";
            Console.WriteLine(command);
            Start_Command.Start_Command_Go(command);
        }

        private void 安装MinGW编译器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start_Command.Start_Command_Go("start C:\\Win-Design\\MinGW-Install.exe");
        }
    }
}
