using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win_Design.Cs;
using Win_Design.Windows;

namespace Win_Design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start_Window start_Window = new Start_Window();
            start_Window.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Design_Main_Window design_Main_Window = new Design_Main_Window();
            design_Main_Window.Show();
            Win32_API.SetParent((IntPtr)design_Main_Window.Get_hWnd(), panel1.Handle);
        }
    }
}
