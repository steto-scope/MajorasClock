using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            datetimepicker.Value = DateTime.Now;
        }

        public CountdownSettings Settings { get; private set; }

        public EditWindow(CountdownSettings s)
        {
            
            InitializeComponent();
            DataContext = s;
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
            ofd.CustomPlaces = new List<FileDialogCustomPlace>()
            {
                new FileDialogCustomPlace(@"C:\Windows\Media\"),
                new FileDialogCustomPlace(AppDomain.CurrentDomain.BaseDirectory+"sounds\\"),
                new FileDialogCustomPlace(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic))
            };
            ofd.Filter = "Audio-Files|*.wav";
            ofd.Multiselect = false;
            bool? result = ofd.ShowDialog();
            if(result.HasValue && result.Value)
            {
                ((CountdownSettings)DataContext).SoundToPlay = ofd.FileName;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            bool? result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ((CountdownSettings)DataContext).ExecuteString = ofd.FileName;
            }
        }
    }
}
