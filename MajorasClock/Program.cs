using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerribleFate;

namespace MajorasClock
{
    static class Program
    {
        static SingleInstance si;
        static MainViewModel mv;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            si = new SingleInstance(new Guid("8CDCCCE6-151F-4D0B-870A-C6CFC2C913B9"));
            if(!si.IsFirstInstance)
            {
                si.PassArgumentsToFirstInstance(args);
                return;
            }
            else
            {
                si.ListenForArgumentsFromSuccessiveInstances();

            }

            ConfigSaveFilePath = Environment.GetEnvironmentVariable("LocalAppData") + "\\MajorasClock";
            CountdownSaveFile = ConfigSaveFilePath + "\\countdowns.xml";
            ConfigSaveFile = ConfigSaveFilePath + "\\config.xml";
            Args = args;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 f = new Form1();
            mv = f.cc.ViewModel;

            si.ArgumentsReceived += (a,b) => 
            {
                if(b.Args.Contains("-m"))
                {
                    mv.StopAllSoundsCommand.Execute(null);
                }
            };            
           
            Application.Run(f);

            
        }

        public static string[] Args { get; private set; }
        public static string CountdownSaveFile { get; private set; }
        public static string ConfigSaveFile { get; private set; }
        public static string ConfigSaveFilePath { get; private set; }

    }
}
