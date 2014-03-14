using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace notifier
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static string Text { get; set; }
        public static int Timeout { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Timeout = 7200;
            Text = "";

            for (int i = 0; i < e.Args.Length; i++)
            {
                string a = e.Args[i].Trim().ToLower();
                if((a == "-t" || a=="-timeout") && i+1<e.Args.Length)
                {
                    int value;
                    if(int.TryParse(e.Args[i + 1], out value))
                    {
                        Timeout = value;
                    }
                }
                if ((a == "-m" || a == "-message") && i + 1 < e.Args.Length)
                {
                    Text = e.Args[i + 1];
                }
            }
                base.OnStartup(e);
        }
    }
}
