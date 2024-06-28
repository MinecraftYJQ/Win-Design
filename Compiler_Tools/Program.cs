using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler_Tools
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(args.Length);
                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);
                Console.WriteLine(args[2]);
                Console.WriteLine(args[3]);
                bool temp = true;
                if (args[3] == "False")
                {
                    temp = false;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(args[0], args[1], args[2], temp));
            }
            catch
            {
                Application.Run(new Form1("","","",false));
            }
        }
    }
}
