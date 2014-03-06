using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TerribleFate
{
    class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is TimeSpan)
            {
                TimeSpan s = (TimeSpan)value;
                
                if (s.TotalSeconds < 1)
                    return "00:00";

                long[] t = new long[5];
                t[0] = s.Seconds;
                t[1] = s.Minutes;
                t[2] = s.Hours;
                t[3] = s.Days % 7;
                t[4] = s.Days / 7;
                char[] ch = "smhdw".ToCharArray();

                StringBuilder sb = new StringBuilder();
                for(int i=t.Length-1; i>=0; i--)
                {
                    if(t[i]>0)
                    {
                        sb.Append(t[i] + ch[i].ToString() + " ");
                    }
                }
                return sb.ToString();

            }

            return "00:00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
