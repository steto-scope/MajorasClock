using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TerribleFate
{
    /// <summary>
    /// Interaktionslogik für EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            DataContext = new CountdownSettings();
            InitializeComponent();
        }

        public CountdownSettings Settings { get; private set; }

        public EditWindow(CountdownSettings s)
        {
            DataContext = s;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings = (CountdownSettings)DataContext;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Audio-Files|*.wav;*.mp3;*.mp4;*.wma";
            ofd.Multiselect = false;
            bool? result = ofd.ShowDialog();
            if(result.HasValue && result.Value)
            {
                sndPath.Text = ofd.FileName;
            }
        }
    }
}
