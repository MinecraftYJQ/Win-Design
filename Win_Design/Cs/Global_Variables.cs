using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Windows;

namespace Win_Design.Cs
{
    internal class Global_Variables
    {
        public static bool Open_Project_Open_Design;
        public static string Open_Project_Open_Path;
        public static string Open_Project_Path;
        public static string json_out;
        public static string json_out_Winsize;
        public static int Main_hWnd;
        public static Panel panel;
        public static Panel Main_Panel;
        public static Form Set_Con=new Form();
        public static Form Set_Win=new Form();
        public static Design_Main_Window design_Main;
        public static class Project_Config
        {
            public static bool Project_C_Command_Window=false;
        }
    }
}
