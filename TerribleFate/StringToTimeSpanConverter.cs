using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TerribleFate
{
    class StringToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                TimeSpan s = (TimeSpan)value;
                return new DateTime().Add(s);
            }
            return new DateTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value is DateTime)
            {
                DateTime s = (DateTime)value;
                return new TimeSpan(s.Hour, s.Minute, s.Second);
            }
            return new TimeSpan();
            
        }
    }
}
