using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Design.Cs.Project
{
    internal class Save_Design_File
    {
        public static bool Save_design_File_Main(string textToWrite)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "设计文件|*.Design"; // 设置文件过滤器
            saveFileDialog.Title = "导出设计文件"; // 设置对话框标题
            saveFileDialog.FileName = "Main"; // 设置默认文件名

            // 显示保存文件对话框
            DialogResult result = saveFileDialog.ShowDialog();

            // 如果用户选择了文件并点击了“保存”按钮
            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // 写入文本到文件
                    File.WriteAllText(filePath, textToWrite);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool Save_C_File_Main(string textToWrite)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "C++源码文件|*.cpp|C语言源码文件|*.c"; // 设置文件过滤器
            saveFileDialog.Title = "导出源码"; // 设置对话框标题
            saveFileDialog.FileName = "Main"; // 设置默认文件名

            // 显示保存文件对话框
            DialogResult result = saveFileDialog.ShowDialog();

            // 如果用户选择了文件并点击了“保存”按钮
            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // 写入文本到文件
                    File.WriteAllText(filePath, textToWrite);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
