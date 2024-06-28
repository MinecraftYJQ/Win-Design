using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Design.Cs.Make_Cpp
{
    internal class Make_Event_Processing_Json
    {
        public static string Make_Event_Processing_Json_Out(Control controls,string sj) {
            string sj_out=sj.Replace("\n", "").Replace("\"","\\\"");//去除换行等字符

            string Json_Out = $"{{\"id\":{controls.Name.Split('|')[1]},\"sj\":\"{sj_out}\"}}";//前面是获取id,后面是拼接字符串
            Console.WriteLine(Json_Out);
            return Json_Out;
        }

        public static bool Write_Event_Processing_Json(string json)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(json);
            int id = obj.id;
            Console.WriteLine($"The ID is: {id}");

            string filePath = Global_Variables.Open_Project_Path + "\\Main.Ep";
            string[] readLines = File.ReadAllLines(filePath);

            // 检查文件是否为空
            if (readLines.Length > 0)
            {
                // 用于存储更新后的内容
                List<string> updatedLines = new List<string>(readLines);

                bool found = false;
                for (int i = 0; i < readLines.Length; i++)
                {
                    var temp_obj = JsonConvert.DeserializeObject<dynamic>(readLines[i]);
                    int temp_id = temp_obj.id;

                    if (temp_id == id)
                    {
                        found = true;
                        // 替换指定行的内容
                        updatedLines[i] = json;
                    }
                }

                if (found)
                {
                    // 将更新后的列表写回到文件
                    File.WriteAllLines(filePath, updatedLines);
                    Console.WriteLine("文件中的指定行已被替换。");
                }
                else
                {
                    // 如果没有找到相同的id，则追加新的内容
                    File.AppendAllText(filePath, json + "\n");
                }
            }
            else
            {
                // 如果文件为空，直接写入新内容
                File.WriteAllText(filePath, json + "\n");
            }

            return true;
        }

        public static string Get_Event_Processing(Control control)
        {
            string[] readlines = File.ReadAllLines(Global_Variables.Open_Project_Path + "\\Main.Ep");
            foreach (string line in readlines)
            {
                var obj = JsonConvert.DeserializeObject<dynamic>(line);
                int id = obj.id;
                Console.WriteLine($"The ID is: {id}");

                if (control.Name.Split('|')[1] == id.ToString())
                {
                    return obj.sj;
                }
            }

            return null;
        }
    }
}
