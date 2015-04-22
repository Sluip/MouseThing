using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MouseKeyboardActivityMonitor.WinApi;



namespace TrackBall
{
    class Program
    {
        MouseListener gay = new MouseListener(); 

        [STAThread]
        static void Main(string[] args)
        {
            gay.Activate();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
