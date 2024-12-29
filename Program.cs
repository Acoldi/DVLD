using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());
            System.Windows.Forms.Application.Run(new frmLogIn());

            // Close properlyo
        }
    }
}
