using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Windows;

namespace Win_Design.Cs.Controls
{
    internal class Control_Actions
    {
        public static void Del_Control(Control control) {
            string nr = Make_Con_Json.Make_Con(control.Name,control.Text,control.Location.X,control.Location.Y,control.Width,control.Height);
            File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.Design", File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design").Replace(nr+ "\n", "").Replace("{\"type\":\"END\"}", "") + "{\"type\":\"END\"}");
            Global_Variables.design_Main.clean_con();
            Open_Project.Make_Design_Window(Global_Variables.design_Main, File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design"));
        }
        
        public static void Del_Window(Control control)
        {
            Form form = new Form();
            form.Width = 100;
            form.Height = 470;
            form.Text = "控件设置";
            form.StartPosition = FormStartPosition.Manual;
            form.Location=new System.Drawing.Point(Global_Variables.design_Main.Width+10,5);
            form.ControlBox = false;
            form.MinimumSize = new System.Drawing.Size(form.Width, form.Height);
            form.MaximumSize = form.MinimumSize;
            //界面控件设计代码
            //删除按钮
            Button btn = new Button();
            btn.Text = "删除";
            btn.Width = 100;
            btn.Height = 30;
            btn.Location = new System.Drawing.Point(10, 10);
            btn.Click += (sender, e) =>
            {
                Del_Control(control);
                form.Close();
            };
            form.Controls.Add(btn);
            //确定按钮
            Button btn2 = new Button();
            btn2.Text = "确定";
            btn2.Width = 100;
            btn2.Height = 30;
            btn2.Location = new System.Drawing.Point(10, form.Height-80);
            //长度设置
            TextBox Wid = new TextBox();
            Wid.Text =control.Width.ToString();
            Wid.Width = 100;
            Wid.Location = new System.Drawing.Point(10, 70);
            form.Controls.Add(Wid);
            //长度设置提示
            Label label = new Label();
            label.Text = "长度设置:";
            label.Location = new System.Drawing.Point(10, 50);
            form.Controls.Add(label);
            //宽度设置
            TextBox Hei = new TextBox();
            Hei.Text =control.Height.ToString();
            Hei.Width = 100;
            Hei.Location = new System.Drawing.Point(10, 120);
            form.Controls.Add(Hei);
            //宽度设置提示
            Label label1 = new Label();
            label1.Text = "宽度设置:";
            label1.Location = new System.Drawing.Point(10, 100);
            form.Controls.Add(label1);
            //左边设置
            TextBox xs = new TextBox();
            xs.Text =control.Location.X.ToString();
            xs.Width = 100;
            xs.Location = new System.Drawing.Point(10, 170);
            form.Controls.Add(xs);
            //左边设置提示
            Label label2 = new Label();
            label2.Text = "左边设置:";
            label2.Location = new System.Drawing.Point(10, 150);
            form.Controls.Add(label2);
            //顶边设置
            TextBox ys = new TextBox();
            ys.Text =control.Location.Y.ToString();
            ys.Width = 100;
            ys.Location=new System.Drawing.Point(10,220);
            form.Controls.Add(ys);
            //顶边设置提示
            Label label3 = new Label();
            label3.Text = "顶边设置:";
            label3.Location = new System.Drawing.Point(10, 200);
            form.Controls.Add(label3);
            //文本设置
            TextBox textset = new TextBox();
            textset.Text = control.Text;
            textset.Width=100;
            textset.Location=new System.Drawing.Point(10,270);
            form.Controls.Add(textset);
            //文本设置提示
            Label label4 = new Label();
            label4.Text = "文本设置:";
            label4.Location = new System.Drawing.Point(10, 250);
            form.Controls.Add(label4);
            //ID设置提示
            Label label5 = new Label();
            label5.Text = "控件ID(可空):";
            label5.Location = new Point(10, 300);
            form.Controls.Add(label5);
            //ID设置输入框
            TextBox textBoxID = new TextBox();
            textBoxID.Text = control.Name.Split('|')[1];
            textBoxID.Location = new Point(10, 320);
            //textBoxID.Enabled = false;//禁用ID设置
            form.Controls.Add(textBoxID);
            //添加时间按钮
            Button button_SJ = new Button();
            button_SJ.Text = "添加点击事件";
            button_SJ.Location = new Point(10, btn2.Location.Y-40);
            button_SJ.Height = 30;
            button_SJ.Width = 100;
            //button_SJ.Enabled = false;//禁用添加事件
            button_SJ.Click += (sender, e) =>
            {
                Add_Event_Processing add_Event_Processing = new Add_Event_Processing(control);
                add_Event_Processing.ShowDialog();
            };
            form.Controls.Add(button_SJ);

            btn2.Click += (sender, e) => {
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
                Control_Actions.Del_Control(control);
                Control new_con = control;
                
                new_con.Location = new System.Drawing.Point(int.Parse(xs.Text),int.Parse(ys.Text));
                new_con.Width = int.Parse(Wid.Text);
                new_con.Height=int.Parse(Hei.Text);
                new_con.Text=textset.Text;
                new_con.Name=new_con.Name.Split('|')[0]+"|"+ textBoxID.Text;
                Global_Variables.design_Main.add_con(new_con);
                Global_Variables.json_out_Winsize = $"{{\"type\":\"Window\",\"wid\":{Global_Variables.design_Main.Width},\"hei\":{Global_Variables.design_Main.Height},\"title\":\"{Global_Variables.design_Main.Text}\"}}";
                File.WriteAllText(Global_Variables.Open_Project_Path+ "\\Main.Design",Global_Variables.json_out_Winsize+File.ReadAllText(Global_Variables.Open_Project_Path + "\\Main.Design").Replace(File.ReadAllLines(Global_Variables.Open_Project_Path + "\\Main.Design")[0],""));
                //File.WriteAllText(Global_Variables.Open_Project_Path + "\\Main.Design", Global_Variables.json_out_Winsize + "\n" + Global_Variables.json_out + "{\"type\":\"END\"}");
                form.Close();
                
            };
            form.Controls.Add(btn2);
            Global_Variables.Set_Con.Close();
            Global_Variables.Set_Win.Close();
            Global_Variables.Set_Con = form;
            form.Show();
            Win32_API.SetParent((IntPtr)form.Handle, (IntPtr)Global_Variables.Main_hWnd);
        }

        public static void Main_Design_Actions()
        {
            //窗体绘制
            Form form = new Form();
            form.Text = "窗体设置";
            form.StartPosition = FormStartPosition.Manual;
            form.ControlBox = false;
            form.Location=new Point(Global_Variables.design_Main.Width+10,5);
            form.Width = 100;
            form.Height = 240;
            form.MaximumSize = form.Size;
            form.MinimumSize = form.Size;

            //设置窗口标题提示
            Label label = new Label();
            label.Text = "窗口标题设置";
            label.Location = new Point(10, 10);
            label.Height = 15;
            form.Controls.Add(label);
            //设置窗口标题输入框
            TextBox textBox = new TextBox();
            textBox.Text = Global_Variables.design_Main.Text;
            textBox.Location = new Point(10, 30);
            form.Controls.Add(textBox);
            //长度设置提示
            Label label1 = new Label();
            label1.Text = "窗体长度设置";
            label1.Location = new Point(10, 60);
            label1.Height = 15;
            form.Controls.Add(label1);
            //长度设置输入框
            TextBox textBox2 = new TextBox();
            textBox2.Text = Global_Variables.design_Main.Width.ToString();
            textBox2.Location = new Point(10, 80);
            form.Controls.Add(textBox2);
            //长度设置提示
            Label label2 = new Label();
            label2.Text = "窗体高度设置";
            label2.Location = new Point(10, 110);
            label2.Height = 15;
            form.Controls.Add(label2);
            //长度设置输入框
            TextBox textBox3 = new TextBox();
            textBox3.Text = Global_Variables.design_Main.Height.ToString();
            textBox3.Location = new Point(10, 130);
            form.Controls.Add(textBox3);
            //确定按钮
            Button button = new Button();
            button.Text = "确定";
            button.Location=new Point(10, 160);
            button.Width = form.Width - 35;
            button.Height = 30;
            button.Click += (sender, e) =>
            {
                Global_Variables.design_Main.Text = textBox.Text;
                Global_Variables.json_out_Winsize = $"{{\"type\":\"Window\",\"wid\":{int.Parse(textBox2.Text)},\"hei\":{int.Parse(textBox3.Text)},\"title\":\"{textBox.Text}\"}}";
                Global_Variables.design_Main.Width = int.Parse(textBox2.Text);
                Global_Variables.design_Main.Height = int.Parse(textBox3.Text);
                form.Close();
            };
            form.Controls.Add(button);

            Global_Variables.Set_Win.Close();
            Global_Variables.Set_Con.Close();
            Global_Variables.Set_Win = form;
            form.Show();
            Win32_API.SetParent(form.Handle, (IntPtr)Global_Variables.Main_hWnd);
        }
    }
}