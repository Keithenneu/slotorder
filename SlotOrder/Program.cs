using System;
using System.Windows.Forms;
using NLog;

namespace SlotOrder
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e);
                MessageBox.Show(@"Unexpected error. See log files for details.");
            }
        }
    }
}
