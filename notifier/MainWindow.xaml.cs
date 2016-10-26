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
    }

}
