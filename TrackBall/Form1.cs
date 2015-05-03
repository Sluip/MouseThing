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

            _rawinput = new RawInput(Handle, false);

            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();

            _rawinput.KeyPressed += OnKeyPressed;
        }
        private void OnKeyPressed(object s, RawInputEventArg e)
        {
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
            }

            //Mouse messages:
            //0x00 = Trackball
            //0x01 Left button down
            //0x02 Left button up
            //0x04 Right button down
            //0x08 Right button up

            //Scroll??
            //Some weird ass address

            //Keyboard
            //Left alt 0x12
            //Usage code 0x8b

            //In case I ever care to work further on it:
            //Need to figure out why hWnd isn't working when not using InputSink for custom windows
            //Faulty way of getting hWnd? 


        }
    }
}
