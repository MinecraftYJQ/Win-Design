using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs;
using Win_Design.Cs.API;
using Win_Design.Cs.Controls;

namespace Win_Design.Windows
{
    public partial class Design_Main_Window : Form
    {
        string json="",size;
        public Design_Main_Window()
        {
            InitializeComponent();
        }
        public void add_con(Control control)
        {
            adds(control);
            //json += $"{{\"type\":\"button\",\"text\":\"{control.Text}\",\"tc\":\"{Get_TcText.Get_TCText(control.Name)}\",\"x\":{control.Location.X},\"y\":{control.Location.Y},\"wid\":{control.Width},\"hei\":{control.Height},\"tcs\":\"hwnd, NULL, hInstance, NULL\"}}\n";
            Console.WriteLine("[debug]" + json);
        }
        public void clean_con()
        {
            panel1.Controls.Clear();
            Width = 300;
            Height = 230;
            size = $"{{\"type\":\"Window\",\"wid\":{Width},\"hei\":{Height},\"title\":\"{Global_Variables.design_Main.Text}\"}}";
            Global_Variables.json_out_Winsize = size;
            json = "";
        }
        private void adds(Control control)
        {
            panel1.Controls.Add(control);
            json += Make_Con_Json.Make_Con(control.Name, control.Text, control.Location.X, control.Location.Y, control.Width, control.Height) + "\n";
            Global_Variables.json_out=json;
        }

        public int Get_hWnd()
        {
            return (int)this.Handle;
        }

        private string GetSize()
        {
            return size;
        }

        public bool Big_Bot(int w,int h,string text, string size)
        {
            size = $"{{\"type\":\"Window\",\"wid\":{w},\"hei\":{h},\"title\":\"{text}\"}}";
            Global_Variables.json_out_Winsize = size;
            
            Width = w ; Height = h ;
            Console.WriteLine($"[debug]{w}|{h}|{text}");

            return true;
        }

        Thread thread;//全局线程,用来检测大小有没有被改变
        private void Design_Main_Window_Load(object sender, EventArgs e)
        {
            int wid=Width, hei=Height;
            size = $"{{\"type\":\"Window\",\"wid\":{wid},\"hei\":{hei},\"title\":\"{Global_Variables.design_Main.Text}\"}}";
            Global_Variables.json_out_Winsize = size;
            thread = new Thread(() =>
            {
                while (true)
                {
                    if (Height != hei || Width != wid)
                    {
                        size = $"{{\"type\":\"Window\",\"wid\":{wid},\"hei\":{hei},\"title\":\"{Global_Variables.design_Main.Text}\"}}";
                        Global_Variables.json_out_Winsize = size;
                        wid = Width;
                        hei = Height;
                        Console.WriteLine($"[debug]{wid}|{hei}|{Text}");
                    }
                }
            });
            thread.Start();
        }

        private void Not_Move_Tick(object sender, EventArgs e)
        {
            //Location=new Point(5, 5);
            Global_Variables.panel.Width = Width+5;
            Global_Variables.panel.Height = Height+5;
            Global_Variables.panel.Location = Location;
        }

        private void Design_Main_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();//一定要加上这个！不然直接CPU占用率飚超级高！！！
            Not_Move.Enabled = false;
            Design_Main_Window design_Main_Window = new Design_Main_Window();
            Global_Variables.design_Main = design_Main_Window;

            Global_Variables.Main_hWnd = (int)Global_Variables.Main_Panel.Handle;
            Console.WriteLine(Global_Variables.Open_Project_Path);
            Open_Project.Make_Design_Window(design_Main_Window, File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design"));

            Global_Variables.design_Main.Show();
            Win32_API.SetParent((IntPtr)design_Main_Window.Get_hWnd(), Global_Variables.Main_Panel.Handle);
            //直接退出,可避免占用CPU使用率,当前版本已修复占用高BUG
            //Environment.Exit(0);
        }
    }
}
