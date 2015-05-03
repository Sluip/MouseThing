using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using RawInput_dll;

namespace TrackBall
{

    class MouseListener
    {

        //private MouseHookListener m_mouseListener;
        //private KeyboardHookListener m_keyboard;
        private int g;
        private bool thing;
        //[DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        //public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern int GetWindowText(int hWnd, StringBuilder title, int size);


        public void Activate(int x, int y)
        {
            
            keybd_event(0x12, 0xb8, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
        }

        public void DeState(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
            keybd_event(0x12, 0xb8, KEYEVENTF_KEYUP, 0);
        }

        public IntPtr WinGetHandle(string wName)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(wName))
                {
                    hWnd = pList.MainWindowHandle;
                    Debug.WriteLine(hWnd);

                }
            }
            return hWnd;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_KEYUP = 0x02;
        public const uint VK_MENU = 0x12;

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, uint dx, uint dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

    }

}
