using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFinderOrganizer
{
    class Program
    {

        [STAThread]
        static void Main()
        {

            var frm = new MainForm();
            Application.EnableVisualStyles();
            Application.Run(frm);
        }
    }
}
