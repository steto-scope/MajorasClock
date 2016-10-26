using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace notifier
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            t.Text = App.Text;
            dt.Interval = TimeSpan.FromSeconds(App.Timeout);
            dt.Tick += dt_Tick;
            dt.Start();

            Loaded += MainWindow_Loaded;

        
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {    
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width -20;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 60;           
        }

        void dt_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mute();
            
            Close();
        }

        private void Mute()
        {
            string p = GetPathOfExecutable();
            if (p != null)
            {
                Process proc = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(p, "-m");
                proc.StartInfo = psi;
                proc.Start();
            }
        }

        private void ResetTime(int seconds)
        {
            string p = GetPathOfExecutable();
            if (p != null)
            {
                Process proc = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(p, "-r "+seconds+" "+App.TimerGuid);
                proc.StartInfo = psi;
                proc.Start();
            }
        }


        private string GetPathOfExecutable()
        {
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcesses()
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    if (item.Process.ProcessName == "MajorasClock")
                        return item.Path;
                }
            }
            return null;
        }

        private void p1_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(60);
            Close();
        }

        private void p2_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(120);
            Close();
        }

        private void p3_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(300);
            Close();
        }

        private void p4_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(600);
            Close();
        }

        private void p5_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(900);
            Close();
        }

        private void p6_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(1800);
            Close();
        }

        private void p7_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(3600);
            Close();
        }

        private void p9_Click(object sender, RoutedEventArgs e)
        {
            ResetTime(0);
            Close();
        }

        private void p8_Click(object sender, RoutedEventArgs e)
        {
            Mute();
            ResetTime(30);
            Close();
        }
    }

}
