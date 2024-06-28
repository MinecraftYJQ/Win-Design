using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win_Design.Cs
{
    internal class Win32_API
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_STYLE = -16;
        public const int WS_CHILD = 0x40000000;
        public const int WS_BORDER = 0x00800000;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        public static int Get_ConsoleWindow()
        {
            IntPtr consoleHandle = GetConsoleWindow();

            return  (int)consoleHandle;
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow=SW_HIDE);

        // 定义窗口显示状态的常量
        private const int SW_HIDE = 0; // 隐藏窗口
    }
}
