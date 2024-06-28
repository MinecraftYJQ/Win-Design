using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Win_Design.Windows;

namespace Win_Design.Cs
{
    internal class Make_CPP
    {
        public static string Make_Win32_Window_CPP(int w,int h,string Window_Json)
        {
            string winsize = $"{w},{h}";
            winsize += ",NULL, NULL, hInstance, NULL);";

            string cmd_window = "";
            /*if (!Global_Variables.Project_Config.Project_C_Command_Window)
            {
                cmd_window = "ShowWindow(hWnd, SW_HIDE);\r\n    ";
            }
            else
            {
                cmd_window = "";
            }*/
            string text1 = $"#include <windows.h>\r\n\r\nLRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)\r\n{{\r\n    switch (msg)\r\n    {{\r\n        case WM_DESTROY:";
            string text3= $"\r\n            PostQuitMessage(0);\r\n            break;\r\n        }}default:\r\n            return DefWindowProc(hwnd, msg, wParam, lParam);\r\n    }}\r\n    return 0;\r\n}}\r\n\r\nint WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)\r\n{{\r\n    HWND hWnd = GetConsoleWindow();\r\n    {cmd_window}WNDCLASS wc = {{0}};\r\n    wc.lpfnWndProc = WndProc;\r\n    wc.hInstance = hInstance;\r\n    wc.lpszClassName = \"Win32ControlsWindow\";\r\n    RegisterClass(&wc);\r\n    HWND hwnd = CreateWindow(wc.lpszClassName, \"{Global_Variables.design_Main.Text}\", WS_OVERLAPPEDWINDOW, 100, 100, ";
            string text2 = $"\n    ShowWindow(hwnd, nCmdShow);\r\n\r\n    MSG msg = {{0}};\r\n    while (GetMessage(&msg, NULL, 0, 0))\r\n    {{\r\n        TranslateMessage(&msg);\r\n        DispatchMessage(&msg);\r\n    }}\r\n\r\n    return msg.wParam;\r\n}}";
            string jgg = "";
            string sjcl = "";
            string num1 = "        case WM_COMMAND:\r\n        {\r\n            int wmId = LOWORD(wParam);\r\n            \r\n            switch (wmId)\r\n            {";
            string num2 = "            }\r\n            break;";


            //解析json来拼接cpp
            try
            {
                string[] lines = Window_Json.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);

                foreach (string jsonline in lines)
                {
                    try
                    {
                        JObject obj = JObject.Parse(jsonline);

                        // 获取指定项的值
                        string type = obj["type"].ToString();
                        string text = obj["text"].ToString();
                        string tc = obj["tc"].ToString();
                        int x = (int)obj["x"];
                        int y = (int)obj["y"];
                        int wid = (int)obj["wid"];
                        int hei = (int)obj["hei"];
                        string tcs = obj["tcs"].ToString();
                        string tcs1 = obj["tcs1"].ToString();
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
                        Console.WriteLine($"[debug]tcs1: {tcs1}");
                        Console.WriteLine($"[debug]id: {id}");

                        //生成事件判断
                        foreach( string lines_id in File.ReadAllLines(Global_Variables.Open_Project_Path + "\\Main.Ep"))
                        {
                            JObject id_ojb = JObject.Parse(lines_id);

                            int ids = (int)id_ojb["id"];
                            if (ids == id)
                            {
                                string outs = $"                case {id}:{(string)id_ojb["sj"]}break;";
                                sjcl +=outs;
                            }
                        }

                        string jg = $"CreateWindow(\"{type.Split('|')[0]}\", \"{ansiString}\", {tc},{x}, {y}, {wid}, {hei}, {tcs},(HMENU){id} ,{tcs1});";
                        jgg += jg + "\n    ";
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
            }catch (Exception e) { Console.WriteLine( e.Message); }
            //string jg = "CreateWindow(\"button\", \"设置文本为123\", WS_VISIBLE | WS_CHILD | BS_PUSHBUTTON,10, 10, 150, 30, hwnd, NULL, hInstance, NULL);";

            return text1 +num1+sjcl+num2+text3+ winsize + "\n\n    "+jgg+text2;
            //HWND hwnd = CreateWindow(wc.lpszClassName, "MAIN WINDOW", WS_OVERLAPPEDWINDOW, 100, 100, 800, 600, NULL, NULL, hInstance, NULL);
        }
    }
}
