using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MouseClicks
{
    public partial class MouseClicksForm : Form
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public MouseClicksForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] destino = {Int32.Parse(this.XPos.Text), Int32.Parse(this.YPos.Text)};
            int[] original = this.obtenerPosicionMouse();
            this.clickear(destino);
            this.mover(original);
        }

        private int[] obtenerPosicionMouse() {
            return new int[] { Cursor.Position.X, Cursor.Position.Y};
        }

        private void clickear(int[] posicion) {
            Cursor.Position = new Point(posicion[0], posicion[1]);
            var input = new INPUT() { dwType = 0, mi = new MOUSEINPUT() { dwFlags = MOUSEEVENTF_LEFTDOWN} };
            SendInput(1, new INPUT[]{input}, Marshal.SizeOf(input));
            input = new INPUT() { dwType = 0, mi = new MOUSEINPUT() { dwFlags = MOUSEEVENTF_LEFTUP } };
            SendInput(1, new INPUT[]{input}, Marshal.SizeOf(input));
        }

        private void mover (int[] posicion) {
            Cursor.Position = new Point(posicion[0], posicion[1]);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT {int dx; int dy; int mouseData; public int dwFlags; int time; IntPtr dwExtraInfo; }
        struct INPUT { public uint dwType; public MOUSEINPUT mi; }
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    }
}
