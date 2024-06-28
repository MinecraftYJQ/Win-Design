using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_Tools.Cs
{
    internal class Start_Command
    {
        public static bool Start_Command_Go(string command)
        {
            // 创建ProcessStartInfo对象，配置启动cmd的参数
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "cmd.exe"; // 指定要启动的程序
            processStartInfo.UseShellExecute = false; // 禁用Shell的使用，以便可以重定向输入输出
            processStartInfo.RedirectStandardInput = true; // 允许将标准输入重定向到cmd
            processStartInfo.RedirectStandardOutput = true; // 允许将标准输出从cmd重定向
            processStartInfo.RedirectStandardError = true; // 允许将标准错误输出从cmd重定向
            processStartInfo.CreateNoWindow = true; // 不创建窗口，后台运行

            // 创建Process对象并启动cmd
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                // 向cmd写入命令
                process.StandardInput.WriteLine(command); // 执行dir命令，列出目录内容
                process.StandardInput.Flush(); // 刷新输入缓冲区，确保命令被发送
                process.StandardInput.Close(); // 关闭输入

                // 读取cmd的输出
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                // 等待cmd命令执行完成
                process.WaitForExit();

                // 打印输出和错误信息
                Console.WriteLine("Output:");
                Console.WriteLine(output);
                Console.WriteLine("Errors:");
                Console.WriteLine(errors);

                // 检查是否有错误发生
                if (!string.IsNullOrEmpty(errors))
                {
                    Console.WriteLine("An error occurred: " + errors);
                }
            }
            return true;
        }
    }
}
