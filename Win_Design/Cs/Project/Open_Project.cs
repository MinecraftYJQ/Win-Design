using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs.Controls;
using Win_Design.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Win_Design.Cs
{
    internal class Open_Project
    {
        public static string Open_Project_Window()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择Win32设计器项目文件";
            openFileDialog.Filter = "项目文件|*.w32project"; // 可以根据需要设置文件过滤器
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 设置初始目录

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                Console.WriteLine("[debug]您选择的项目是：" + selectedFilePath);
                return selectedFilePath;
            }
            else
            {
                Console.WriteLine("[debug]未选择任何项目");
                return "not file";
            }
        }

        public static void Cick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Control_Actions.Del_Window(control);
        }
        public static bool Make_Design_Window(Design_Main_Window design_Main_Window,string Json) {
            try
            {
                string[] lines = Json.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
                Global_Variables.json_out = "";
                foreach (string jsonline in lines)
                {
                    ////Thread.Sleep(50);
                    try
                    {
                        JObject obj = JObject.Parse(jsonline);

                        // 获取指定项的值
                        string type = obj["type"].ToString();

                        if (type != "Window"&&type!="END")
                        {
                            string text = obj["text"].ToString();
                            string tc = obj["tc"].ToString();
                            int x = (int)obj["x"];
                            int y = (int)obj["y"];
                            int wid = (int)obj["wid"];
                            int hei = (int)obj["hei"];
                            string tcs = obj["tcs"].ToString();
                            int id = (int)obj["id"];

                            string chineseText = Regex.Unescape(text);

                            // 将字符串转换为ANSI编码的字节数组
                            byte[] ansiBytes = Encoding.GetEncoding("gb2312").GetBytes(chineseText);

                            // 将ANSI编码的字节数组转换为字符串
                            string ansiString = Encoding.GetEncoding("gb2312").GetString(ansiBytes);

                            // 输出指定项的值
                            Console.WriteLine($"[debug]type: {type}");
                            Console.WriteLine($"[debug]text: {ansiString}");
                            Console.WriteLine($"[debug]tc: {tc}");
                            Console.WriteLine($"[debug]x: {x}");
                            Console.WriteLine($"[debug]y: {y}");
                            Console.WriteLine($"[debug]wid: {wid}");
                            Console.WriteLine($"[debug]hei: {hei}");
                            Console.WriteLine($"[debug]tcs: {tcs}");
                            Console.WriteLine($"[debug]id: {id}");

                            switch (type)
                            {
                                case "Static":
                                    Label Static = new Label();
                                    Static.Name = $"Static|{id}";
                                    Static.Text = text;
                                    Static.Location = new Point(x, y);
                                    Static.Width = wid;
                                    Static.Height = hei;
                                    Static.Click+= new System.EventHandler(Cick);
                                    design_Main_Window.add_con(Static);
                                    break;
                                case "Button":
                                    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
                                    button.Name = $"Button|{id}";
                                    button.Text = text;
                                    button.Location = new Point(x, y);
                                    button.Width = wid;
                                    button.Height = hei;
                                    button.Click += new System.EventHandler(Cick);
                                    design_Main_Window.add_con(button);
                                    break;
                                case "Edit":
                                    System.Windows.Forms.TextBox Edit = new System.Windows.Forms.TextBox();
                                    Edit.Name = $"Edit|{id}";
                                    Edit.Text = text;
                                    Edit.Location = new Point(x, y);
                                    Edit.Width = wid;
                                    Edit.Height = hei;
                                    Edit.Click += new System.EventHandler(Cick);
                                    design_Main_Window.add_con(Edit);
                                    break;
                            }
                        }
                        else if(type=="Window")
                        {
                            int wid = (int)obj["wid"];
                            int hei = (int)obj["hei"];
                            string title = (string)obj["title"];

                            Console.WriteLine($"[debug]wid: {wid}");
                            Console.WriteLine($"[debug]hei: {hei}");
                            Console.WriteLine($"[debug]title: {title}");
                            design_Main_Window.Width = wid;
                            design_Main_Window.Height = hei;
                            design_Main_Window.Text=title;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Error]{ex.Message}");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }
    }
}
