using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs;
using Win_Design.Cs.Controls;
using Win_Design.Cs.Project;
using Win_Design.Windows;

namespace Win_Design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Design_Main_Window design_Main_Window = new Design_Main_Window();
        private void Form1_Load(object sender, EventArgs e)
        {
            Win32_API.ShowWindow((IntPtr)Win32_API.Get_ConsoleWindow());

            Form.CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls= false;
            Task.Run(() =>
            {
                Thread.Sleep(100);
                while (true)
                {
                    Text = "项目设计器 - Win-Design 作者：B站 Minecraft一角钱";
                    Thread.Sleep(1000);
                }
            });
            Global_Variables.panel = panel2;
            Global_Variables.Open_Project_Open_Design = false;
            Visible = false;
            ShowInTaskbar = false;
            Start_Window start_Window = new Start_Window();
            start_Window.ShowDialog();
            Global_Variables.design_Main = design_Main_Window;

            while (true)
            {
                if (Global_Variables.Open_Project_Open_Design)
                {
                    Visible = true;
                    ShowInTaskbar = true;
                    break;
                }
                else
                {
                    start_Window.ShowDialog();
                }
            }
            Global_Variables.design_Main.Show();
            Win32_API.SetParent((IntPtr)design_Main_Window.Get_hWnd(), panel1.Handle);

            Global_Variables.Main_Panel = panel1;
            Global_Variables.Main_hWnd = (int)panel1.Handle;
            Console.WriteLine(Global_Variables.Open_Project_Path);
            Open_Project.Make_Design_Window(design_Main_Window, File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Button button = new Button();

            button.Name = "Button";
            button.Text = "Hello";
            button.Location = new Point(10, 10);
            button.Width = 100;
            button.Height = 30;

            design_Main_Window.add_con(button);*/
            Setting_Add_Control setting_Add_Control = new Setting_Add_Control(design_Main_Window);
            setting_Add_Control.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.Design",Global_Variables.json_out_Winsize+"\n"+Global_Variables.json_out+"{\"type\":\"END\"}");
            string cpp = Make_CPP.Make_Win32_Window_CPP(Global_Variables.design_Main.Width,Global_Variables.design_Main.Height, Global_Variables.json_out);
            Console.WriteLine("[debug]" + cpp);/*
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.cpp", cpp);
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.c", cpp);*/
            using (StreamWriter writer = new StreamWriter(Global_Variables.Open_Project_Path + "\\Main.cpp", false, Encoding.Default))
            {
                writer.WriteLine(cpp);
            }
            using (StreamWriter writer = new StreamWriter(Global_Variables.Open_Project_Path + "\\Main.c", false, Encoding.Default))
            {
                writer.WriteLine(cpp);
            }
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.Config",$"{{\"Command_Window\":\"{Global_Variables.Project_Config.Project_C_Command_Window}\"}}");
            MessageBox.Show("界面源码已生成到项目文件夹内，请打开目标文件进行编译！","提示",MessageBoxButtons.OK, MessageBoxIcon.Warning);  
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Start_Window start_Window = new Start_Window();
            start_Window.ShowDialog();
            Global_Variables.design_Main.clean_con();
            Global_Variables.design_Main.Width = 300;
            Global_Variables.design_Main.Height = 230;

            Console.WriteLine(Global_Variables.Open_Project_Path);
            Open_Project.Make_Design_Window(design_Main_Window, File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design"));
        }

        private void 关于此程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox1 = new AboutBox();
            aboutBox1.ShowDialog();
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/MinecraftYJQ/Win-Design";

            // 启动默认浏览器打开网址
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }

        private void bilibiliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://space.bilibili.com/1527364468";

            // 启动默认浏览器打开网址
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }

        private void 官网ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://minecraftyjq.github.io/html/Win-Design";

            // 启动默认浏览器打开网址
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }

        private void 导入设计文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import_Design_File.Make_Import_Design_Window_Main(Import_Design_File.Open_File());
        }

        private void 导出设计文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_Design_File.Save_design_File_Main(File.ReadAllText(Global_Variables.Open_Project_Path+"\\Main.Design"));
        }

        private void 显示控制台窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (显示控制台窗口ToolStripMenuItem.Checked)
            {
                Win32_API.ShowWindow((IntPtr)Win32_API.Get_ConsoleWindow(),0);
                显示控制台窗口ToolStripMenuItem.Checked = false;
            }
            else
            {
                Win32_API.ShowWindow((IntPtr)Win32_API.Get_ConsoleWindow(),1);
                显示控制台窗口ToolStripMenuItem.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Control_Actions.Main_Design_Actions();
        }

        private void 导出源码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.Design", Global_Variables.json_out_Winsize + "\n" + Global_Variables.json_out + "{\"type\":\"END\"}");
            string cpp = Make_CPP.Make_Win32_Window_CPP(Global_Variables.design_Main.Width, Global_Variables.design_Main.Height, Global_Variables.json_out);
            Console.WriteLine("[debug]" + cpp);/*
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.cpp", cpp);
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.c", cpp);*/
            using (StreamWriter writer = new StreamWriter(Global_Variables.Open_Project_Path + "\\Main.cpp", false, Encoding.Default))
            {
                writer.WriteLine(cpp);
            }
            using (StreamWriter writer = new StreamWriter(Global_Variables.Open_Project_Path + "\\Main.c", false, Encoding.Default))
            {
                writer.WriteLine(cpp);
            }
            Save_Design_File.Save_C_File_Main(File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.cpp"));
        }

        private void 打开项目文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Global_Variables.Open_Project_Path;

            // 启动默认浏览器打开网址
            Process.Start(new ProcessStartInfo("explorer", $"{path}")
            {
                CreateNoWindow = true
            });
        }

        private void 项目设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project_Setting_Window project_Setting_Window = new Project_Setting_Window();
            project_Setting_Window.ShowDialog();
        }
    }
}
