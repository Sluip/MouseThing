using System;
using RawInput_dll;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace TrackBall
{
    public partial class Form1 : Form
    {
        private readonly RawInput _rawinput;
        public Form1()
        {
            InitializeComponent();

            _rawinput = new RawInput(Handle, false);

            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
         // Writes a file DeviceAudit.txt to the current directory

            Win32.DeviceAudit();
            _rawinput.KeyPressed += OnKeyPressed;   
        }
        private void OnKeyPressed(object s, RawInputEventArg e)
        {
            if (e.KeyPressEvent.DeviceName.StartsWith(@"\\?\HID#VID_046D&PID_C52B&MI"))
                Console.WriteLine("Fuck yeah");
        }
    }
}
