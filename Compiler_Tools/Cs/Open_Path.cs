using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler_Tools.Cs
{
    internal class Open_Path
    {
        public static string Get_Open_File_Path()
        {
            // 创建OpenFileDialog实例
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的标题
            openFileDialog.Title = "打开文件";

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            openFileDialog.Filter = "C++文件 (*.cpp)|*.cpp|C语音文件 (*.c)|*.c";

            // 显示对话框并获取用户选择
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // 用户选择了文件，openFileDialog.FileName将包含所选文件的路径
                string selectedFilePath = openFileDialog.FileName;
                // openFileDialog.FileTypes 可以获取所选文件的类型信息

                Console.WriteLine("选择的文件路径是: " + selectedFilePath);
                // 这里可以添加代码来打开或处理所选文件
                return selectedFilePath;
            }
            else
            {
                Console.WriteLine("用户取消了选择");

                return "not file";
            }
        }

        public static string Get_Porject_Out_Path() {
            // 创建FolderBrowserDialog实例
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // 设置对话框的描述
            folderBrowserDialog.Description = "请选择一个文件夹";

            // 显示对话框并获取用户选择
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // 用户选择了文件夹，folderBrowserDialog.SelectedPath将包含所选文件夹的路径
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                Console.WriteLine("选择的文件夹路径是: " + selectedFolderPath);
                return selectedFolderPath;
            }
            else
            {
                Console.WriteLine("用户取消了选择");

                return "not file";
            }
        }
    }
}
