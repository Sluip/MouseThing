using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Runtime.InteropServices;
using RawInput_dll;

namespace TrackBall
{

    class MouseListener
    {
        private MouseHookListener m_mouseListener;
        private KeyboardHookListener m_keyboard;
        private int g;
        private bool thing;
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(int hWnd, StringBuilder title, int size);


        public void Activate()
        {
            thing = false;
            // Note: for an application hook, use the AppHooker class instead
            //m_mouseListener = new MouseHookListener(new GlobalHooker());
            // The listener is not enabled by default
            //m_mouseListener.Enabled = true;

           // IntPtr hwnd = Process.GetCurrentProcess().MainWindowHandle;
            IntPtr hwnd = WinGetHandle("Maya");

            Console.WriteLine(hwnd);

            RawInput rawinput = new RawInput(hwnd, false);
            rawinput.KeyPressed += OnKeyPressed;
            // Set the event handler
            // recommended to use the Extended handlers, which allow input suppression among other additional information
           // m_mouseListener.MouseDownExt += MouseListener_MouseDownExt;
           // m_mouseListener.MouseMoveExt += MouseListener_MouseMoveExt;
        }

        public void Deactivate()
        {
            m_mouseListener.Dispose();
        }
        public static IntPtr WinGetHandle(string wName)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(wName))
                {
                    hWnd = pList.MainWindowHandle;

                }
            }
            return hWnd;
        }
        private void OnKeyPressed(object s, RawInputEventArg e)
        {
            Console.WriteLine("hello?");
        }
        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            // log the mouse click

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Console.WriteLine(thing);
                mouse_event(MOUSEEVENTF_LEFTDOWN, e.X, e.Y, 0, 0);
                keybd_event(0x12, 0xb8, 0, 0);
                thing = false;
            }
            //else if (e.Button == System.Windows.Forms.MouseButtons.Right && !thing)
            //{
            //    mouse_event(MOUSEEVENTF_LEFTUP, e.X, e.Y, 0, 0);
            //    keybd_event(0x12, 0xb8, KEYEVENTF_KEYUP, 0);
            //    thing = true;
            //}

            // uncommenting the following line with suppress a middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_KEYUP = 0x02;
        public const uint VK_MENU = 0x12;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private void MouseListener_MouseMoveExt(object sender, MouseEventExtArgs e)
        {

            //if (thing && !thing2)
            //{
            //    mouse_event(MOUSEEVENTF_LEFTDOWN, e.X, e.Y, 0, 0);
            //    keybd_event(0x12, 0xb8, 0, 0);
            //    thing2 = true;
            //}
            //else if (!thing && thing2)
            //{
            //    mouse_event(MOUSEEVENTF_LEFTUP, e.X, e.Y, 0, 0);
            //    keybd_event(0x12, 0xb8, KEYEVENTF_KEYUP, 0);
            //    thing2 = false;
            //}

            //Console.WriteLine("Fuck off {0}", g);
        }
    }

}
