using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TerribleFate
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            ConfigSaveFilePath = Environment.GetEnvironmentVariable("LocalAppData")+"\\MajorasClock";
            CountdownSaveFile = ConfigSaveFilePath+"\\countdowns.xml";
            ConfigSaveFile = ConfigSaveFilePath+ "\\config.xml";                
        }

        public static string CountdownSaveFile { get; private set; }
        public static string ConfigSaveFile { get; private set; }

        public static string ConfigSaveFilePath { get; private set; }

    }
}
