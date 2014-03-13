using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TerribleFate
{
    /// <summary>
    /// Interaktionslogik für CountdownControl.xaml
    /// </summary>
    public partial class CountdownControl : UserControl
    {
        public CountdownControl()
        {
            InitializeComponent();
            var mw = new MainViewModel();
            mw.Load();
            DataContext = mw;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata( XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag))); 
        }



        public MainViewModel ViewModel
        {
            get
            {
                return DataContext as MainViewModel;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.IsMouseOver = true;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.IsMouseOver = false;
        }

        private void Grid_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Grid i = sender as Grid;
            if(i!=null)
            {
                ((Countdown)i.DataContext).IsMouseOver = true;
            }
        }

        private void Grid_MouseLeave_1(object sender, MouseEventArgs e)
        {
            try
            {
                Grid i = sender as Grid;
                if (i != null)
                {
                    ((Countdown)i.DataContext).IsMouseOver = false;
                }
            }
            catch { }
        }

    }
}
