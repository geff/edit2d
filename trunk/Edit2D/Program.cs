using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Edit2D
{
    static class Program
    {
        public static FrmEdit2D frm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frm = new FrmEdit2D();
            Application.Run(frm);
        }
    }
}