using System;
using RawInput_dll;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TrackBall
{
    public partial class Form1 : Form
    {
        private readonly RawInput _rawinput;
        private MouseListener _mouseListener;

        private bool _rotation;

        public Form1()
        {
            InitializeComponent();
            _rotation = true;
            _mouseListener = new MouseListener();

            IntPtr hwnd = _mouseListener.WinGetHandle("Autodesk Maya 2015");

            _rawinput = new RawInput(Handle, false);

            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();

            _rawinput.KeyPressed += OnKeyPressed;
        }
        private void OnKeyPressed(object s, RawInputEventArg e)
        {
            //Console.WriteLine(e.KeyPressEvent.DeviceName);
            if (e.KeyPressEvent.DeviceName.StartsWith(@"\\?\HID#VID_046D&PID_C52B&MI") && e.KeyPressEvent.Message == 0)
            {
                if (_rotation)
                {
                    _mouseListener.Activate(e.KeyPressEvent.MouseX, e.KeyPressEvent.MouseY);
                    _rotation = false;
                }

                Console.WriteLine("Rotating you fuck");
            }
            else if (e.KeyPressEvent.DeviceName != "Global Keyboard" && !_rotation)
            {
                _mouseListener.DeState(e.KeyPressEvent.MouseX, e.KeyPressEvent.MouseY);
                _rotation = true;
                Console.WriteLine(e.KeyPressEvent.KeyPressState);
                
            }
        }
    }
}
