using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajorasClock
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ConfigSaveFilePath = Environment.GetEnvironmentVariable("LocalAppData") + "\\MajorasClock";
            CountdownSaveFile = ConfigSaveFilePath + "\\countdowns.xml";
            ConfigSaveFile = ConfigSaveFilePath + "\\config.xml";
            Args = args;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static string[] Args { get; private set; }
        public static string CountdownSaveFile { get; private set; }
        public static string ConfigSaveFile { get; private set; }
        public static string ConfigSaveFilePath { get; private set; }

    }
}
