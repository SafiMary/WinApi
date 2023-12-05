using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinApi
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hWnd, string Text, string Caption, int Options);

        [DllImport("user32.dll")]
        public static extern bool SetWindowTextA(IntPtr hWnd, string Caption);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowW(string ClassName, string WindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPStr)] string Param);
        const uint WM_SETEXT = 0x0C;

        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int GetClassName (IntPtr hWnd, StringBuilder ClassName,int Count);
        private string GetClassName(IntPtr hWnd)
        {
            StringBuilder ClassName = new StringBuilder(256);
            GetClassName(hWnd,ClassName, ClassName.Capacity);
            return ClassName.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox(IntPtr.Zero, "Hello World!!", "Test", 0);
            SetWindowTextA(this.Handle, "Поехали!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = FindWindowW(null, WinName.Text);
            //SetWindowTextA(hWnd,NewCaption.Text);
            SendMessage(hWnd, WM_SETEXT, 0, NewCaption.Text);
        }
    

        string GetWindowText(IntPtr hWnd)
        {
            int len = GetWindowTextLength(hWnd) + 1;
            StringBuilder sb = new StringBuilder(len);
            len = GetWindowText(hWnd, sb, len);
            return sb.ToString(0, len);
        }
    

    private void button3_Click(object sender, EventArgs e)
        {
           EnumWindows((hWnd, lParam) => {
                if (IsWindowVisible(hWnd) && GetWindowTextLength(hWnd) != 0)
                {
                    Window_s.Text+= "Класс= "+ GetClassName(hWnd) + " Название= "+ GetWindowText(hWnd)+"\r\n";
                }
                return true;
            }, IntPtr.Zero);
        }
    }
}