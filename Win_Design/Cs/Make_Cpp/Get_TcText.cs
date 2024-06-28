using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win_Design.Cs.API
{
    internal class Get_TcText
    {
        public static string Get_TCText(string type)
        {
            switch (type)
            {
                case "Button":
                    return "WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON";
                case "Edit":
                    return "WS_CHILD | WS_VISIBLE | WS_BORDER | ES_AUTOHSCROLL";
                case "Static":
                    return "WS_CHILD | WS_VISIBLE | SS_CENTER";
            }
            return null;
        }
    }
}
