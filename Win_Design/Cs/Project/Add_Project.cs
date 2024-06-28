using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win_Design.Cs
{
    internal class Add_Project
    {
        public static bool Add_Project_Main(string path, string name) {
            if (!Directory.Exists(path+name))
            {
                Directory.CreateDirectory(path+"\\"+name);
                File.WriteAllText($"{path}\\{name}\\{name}.w32project", "Main.cpp\nMain.c\nMain.Design\nMain.Config");
                File.WriteAllText($"{path}\\{name}\\Main.cpp", "");
                File.WriteAllText($"{path}\\{name}\\Main.c", "");
                File.WriteAllText($"{path}\\{name}\\Main.Design", "");
                File.WriteAllText($"{path}\\{name}\\Main.Config", $"{{\"Command_Window\":false}}");
                File.WriteAllText($"{path}\\{name}\\Main.Ep", "");
                return true;
            }
            return false;
        }
    }
}
