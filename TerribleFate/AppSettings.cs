﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TerribleFate
{
    [XmlInclude(typeof(SolidColorBrush))]
    [XmlInclude(typeof(MatrixTransform))]
    public class AppSettings : BaseObject
    {

        static AppSettings()
        {
            ConfigSaveFilePath = Environment.GetEnvironmentVariable("LocalAppData")+"\\MajorasClock";
            CountdownSaveFile = ConfigSaveFilePath+"\\countdowns.xml";
            ConfigSaveFile = ConfigSaveFilePath+ "\\config.xml";                
        }

        public static string CountdownSaveFile { get; private set; }
        public static string ConfigSaveFile { get; private set; }

        public static string ConfigSaveFilePath { get; private set; }

        public Brush TextBrush
        {
            get { return Get<Brush>("TextBrush"); }
            set { Set("TextBrush", value); }
        }

        public double Left
        {
            get { return Get<double>("Left"); }
            set { Set("Left", value); }
        }

        public double Top
        {
            get { return Get<double>("Top"); }
            set { Set("Top", value); }
        }

        public AppSettings()
        {
            TextBrush = new SolidColorBrush(Colors.White);
            Left = 0;
            Top = 0;
        }

        public void UpdateHeaderColor(bool value)
        {
            useLockedColor = value;
            OnPropertyChanged("HeaderColor");
        }

        private bool useLockedColor;
        private static SolidColorBrush unlockedheader = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        private static SolidColorBrush lockedheader = new SolidColorBrush(Colors.Transparent);
        public Brush HeaderColor
        {
            get
            {
                if (useLockedColor)
                    return lockedheader;
                return unlockedheader;
            }
            set
            {
                OnPropertyChanged("HeaderColor");
            }
        }

        [XmlElement("Locked")]
        public bool LockedSerialized { get; set; }
    }
}
