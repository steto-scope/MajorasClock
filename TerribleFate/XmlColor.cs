using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TerribleFate
{
    public class XmlColor
    {
        private Color color_ = Colors.Black;

        public XmlColor() { }
        public XmlColor(Color c) { color_ = c; }


        public Color ToColor()
        {
            return color_;
        }

        public void FromColor(Color c)
        {
            color_ = c;
        }

        public static implicit operator Color(XmlColor x)
        {
            return x.ToColor();
        }

        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        [XmlAttribute]
        public string Value
        {
            get
            {
                return color_.ToString();
            }
            set
            {
                try
                {
                    color_ = (Color)ColorConverter.ConvertFromString(value);
                }
                catch
                {

                }
            }
        }


    }
}
