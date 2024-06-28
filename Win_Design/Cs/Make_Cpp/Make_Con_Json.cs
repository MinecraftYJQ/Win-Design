using System;
using System.Text.Json;
using Win_Design.Cs.API;

namespace Win_Design.Cs
{
    internal class Make_Con_Json
    {
        public static string Make_Con(string type,string text,int x,int y,int wid,int hei)
        {
            var buttonData = new
            {
                type = type.Split('|')[0],
                text = text,
                tc = Get_TcText.Get_TCText(type.Split('|')[0]),
                x = x,
                y = y,
                wid = wid,
                hei = hei,
                tcs = "hwnd",
                tcs1="hInstance, NULL",
                id = int.Parse(type.Split('|')[1])
            };

            string json = JsonSerializer.Serialize(buttonData, new JsonSerializerOptions { WriteIndented = false });
            Console.WriteLine(json);
            return json;
        }
    }
}