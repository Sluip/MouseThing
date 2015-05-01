﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.RawInput;
using SharpDX.Multimedia;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Runtime.InteropServices;

namespace TrackBall
{

    class MouseListener
    {
        private MouseHookListener m_mouseListener;
        private Device m_mouseDevice;
        private int g;
        public void Activate()
        {
            // Note: for an application hook, use the AppHooker class instead
            m_mouseListener = new MouseHookListener(new GlobalHooker());
            // The listener is not enabled by default
            m_mouseListener.Enabled = true;
            List<DeviceInfo> devices = Device.GetDevices();

            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
            Device.MouseInput += (sender, e) => MouseEvent(e);
            Device.RawInput += (object sender, RawInputEventArgs e) => RawEvent(e);
            foreach (DeviceInfo s in devices)
            {
                Console.WriteLine(s.Handle + ", " + s.ToString());
            }
            // Set the event handler
            // recommended to use the Extended handlers, which allow input suppression among other additional information
            m_mouseListener.MouseDownExt += MouseListener_MouseDownExt;
            m_mouseListener.MouseMoveExt += MouseListener_MouseMoveExt;
        }

        public void Deactivate()
        {
            m_mouseListener.Dispose();
        }

        static void MouseEvent(RawInputEventArgs e)
        {
            var a = (MouseInputEventArgs)e;
            Console.WriteLine(a.Device);
        }
        static void RawEvent(RawInputEventArgs e)
        {
            Console.WriteLine(e.Device);
        }
        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            // log the mouse click
            Console.WriteLine(string.Format("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.WheelScrolled, e.Timestamp));


            // uncommenting the following line with suppress a middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const uint KEYEVENTF_KEYUP = 0x02;
        public const uint VK_ALT = 0x11;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x08;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private void MouseListener_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            bool g = false;
            if (!g)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, e.X, e.Y, 0, 0);
                g = true;
            }

            //Console.WriteLine("Fuck off {0}", g);
        }
    }

}
