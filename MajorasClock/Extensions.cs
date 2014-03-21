using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorasClock
{
    public static class Extensions
    {
        public static string ToCountdownString(this TimeSpan s)
        {
            if (s != null)
            {
                if (s.TotalSeconds < 1)
                    return " - ";

                long[] t = new long[5];
                t[0] = s.Seconds;
                t[1] = s.Minutes;
                t[2] = s.Hours;
                t[3] = s.Days % 7;
                t[4] = s.Days / 7;
                char[] ch = "smhdw".ToCharArray();

                StringBuilder sb = new StringBuilder();
                for (int i = t.Length - 1; i >= 0; i--)
                {
                    if (t[i] > 0)
                    {
                        sb.Append(t[i] + ch[i].ToString() + " ");
                    }
                }
                return sb.ToString();
            }

            return " - ";
        }

        public static string ToCountdownString(this long s)
        {
            if (s != null)
            {
                if (s < 1)
                    return " - ";

                long[] t = new long[5];
                t[0] = s % 60;
                char[] ch = "smhdw".ToCharArray();

                StringBuilder sb = new StringBuilder();
                for (int i = t.Length - 1; i >= 0; i--)
                {
                    if (t[i] > 0)
                    {
                        sb.Append(t[i] + ch[i].ToString() + " ");
                    }
                }
                return sb.ToString();
            }

            return " - ";
        }
    }
}
