using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Windows;

namespace Win_Design.Cs.Project
{
    internal class Import_Design_File
    {
        public static string Open_File()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择Win32设计文件";
            openFileDialog.Filter = "设计文件|*.Design"; // 可以根据需要设置文件过滤器
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 设置初始目录

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                Console.WriteLine("[debug]您选择的文件是：" + selectedFilePath);
                return selectedFilePath;
            }
            else
            {
                Console.WriteLine("[debug]未选择任何文件");
                return "not file";
            }
        }

        public static bool Make_Import_Design_Window_Main(string path)
        {
            if(path=="not file")
            {
                return false;
            }
            Global_Variables.design_Main.clean_con();
            Global_Variables.design_Main.Width = 300;
            Global_Variables.design_Main.Height = 230;

            Console.WriteLine(path);
            File.WriteAllText(Global_Variables.Open_Project_Path+"\\Main.Design",File.ReadAllText(path));
            Open_Project.Make_Design_Window(Global_Variables.design_Main, File.ReadAllText(path));

            return true;
        }
    }
}
