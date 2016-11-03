using System;
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
        public static AppSettings C { get; set; }

        static AppSettings()
        {
            ConfigSaveFilePath = Environment.GetEnvironmentVariable("LocalAppData")+"\\MajorasClock";
            CountdownSaveFile = ConfigSaveFilePath+"\\countdowns.xml";
            ConfigSaveFile = ConfigSaveFilePath+ "\\config.xml";                
        }

        public static string CountdownSaveFile { get; private set; }
        public static string ConfigSaveFile { get; private set; }

        public static string ConfigSaveFilePath { get; private set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Color TextColor
        {
            get { return Get<Color>("TextColor"); }
            set { Set("TextColor", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ButtonColor1
        {
            get { return Get<Color>("ButtonColor1"); }
            set { Set("ButtonColor1", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ButtonColor2
        {
            get { return Get<Color>("ButtonColor2"); }
            set { Set("ButtonColor2", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color TrashButtonColor1
        {
            get { return Get<Color>("TrashButtonColor1"); }
            set { Set("TrashButtonColor1", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color TrashButtonColor2
        {
            get { return Get<Color>("TrashButtonColor2"); }
            set { Set("TrashButtonColor2", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ToggleButtonColor1
        {
            get { return Get<Color>("ToggleButtonColor1"); }
            set { Set("ToggleButtonColor1", value); }
        }

        [XmlElement(Type = typeof(XmlColor))]
        public Color ToggleButtonColor2
        {
            get { return Get<Color>("ToggleButtonColor2"); }
            set { Set("ToggleButtonColor2", value); }
        }


        [XmlElement(Type = typeof(XmlColor))]
        public Color TextBorderColor
        {
            get { return Get<Color>("TextBorderColor"); }
            set { Set("TextBorderColor", value); }
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
            TextColor = Colors.White;
            TextBorderColor = Colors.Black;
            ButtonColor1 = (Color)ColorConverter.ConvertFromString("#FF888888");
            ButtonColor2 = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            TrashButtonColor1 = (Color)ColorConverter.ConvertFromString("#FFFF8888");
            TrashButtonColor2 = (Color)ColorConverter.ConvertFromString("#FFFF0000");
            ToggleButtonColor1 = (Color)ColorConverter.ConvertFromString("#FFFFa588");
            ToggleButtonColor2 = (Color)ColorConverter.ConvertFromString("#FFFFa500");

            Left = 0;
            Top = 0;

            C = this;
        }


        [XmlElement("Locked")]
        public bool LockedSerialized { get; set; }
    }
}
