using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace D2DMapEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new D2DMapEditor());
        }
    }
}