using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace CASTruck
{
    static class Program
    {
        #region 

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            IntPtr nWnd = FindWindow(null, Constants.TITLE_APP);
            if (nWnd.Equals(new System.IntPtr(0)))
            {
                Application.Run(new mdiMain());
            }
            else
            {
                SetForegroundWindow(nWnd);
                Application.Exit();
            }
        }
    }
}
