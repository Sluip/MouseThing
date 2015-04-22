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
        

        [STAThread]
        static void Main(string[] args)
        {
            MouseListener gay = new MouseListener(); 
            gay.Activate();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
