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

        public bool Locked
        {
            get { return Get<bool>("Locked"); }
            set { Set("Locked", value); OnPropertyChanged("HeaderColor"); }
        }

        private static SolidColorBrush unlockedheader = new SolidColorBrush(Color.FromArgb(20, 255, 255, 255));
        private static SolidColorBrush lockedheader = new SolidColorBrush(Colors.Transparent);
        public Brush HeaderColor
        {
            get
            {
                if (Locked)
                    return lockedheader;
                return unlockedheader;
            }
        }
    }
}
