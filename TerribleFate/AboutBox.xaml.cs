using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaktionslogik für AboutBox.xaml
    /// </summary>
    public partial class AboutBox : Window
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://terriblefate.codeplex.com/");
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://p.yusukekamiyamane.com/");
        }
    }
}
