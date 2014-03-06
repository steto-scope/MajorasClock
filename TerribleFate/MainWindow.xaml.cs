using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TerribleFate
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO Logo
        //TODO Kontextmenü
        //TODO Instanzen im EditWindow entkoppeln
       
        public MainWindow()
        {
            var mw = new MainViewModel();
            mw.CloseRequest += mw_CloseRequest;
            DataContext = mw;

            InitializeComponent();
        }

        void mw_CloseRequest(object sender, EventArgs e)
        {
            Close();
        }




        #region Desktop-Integration-Stuff

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainViewModel mw = DataContext as MainViewModel;
            if(e.LeftButton == MouseButtonState.Pressed && mw!=null && !mw.Locked)
                DragMove();
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new System.EventHandler(t_Tick);
            t.Interval = new System.TimeSpan(0, 0, 0, 0, 1);
            t.Start();
        }


        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_NOZORDER = 0x0004;
        const int WM_WINDOWPOSCHANGING = 0x0046;
        bool istop = false;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        static readonly IntPtr HWND_TOP = new IntPtr(-1);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        public void SetBottom(Window window)
        {
            SetWindowPos(new WindowInteropHelper(this).Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
            istop = false;
        }
        public void SetTop(Window window)
        {
            SetWindowPos(new WindowInteropHelper(this).Handle, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            istop = true;
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        void t_Tick(object sender, System.EventArgs e)
        {
            IntPtr self;
            IntPtr foreground;
            IntPtr hWndProgMan = FindWindow("Progman", "Program Manager");
            IntPtr workerw = FindWindow("WorkerW", "");
            WindowInteropHelper helper = new WindowInteropHelper(this);
            self = helper.Handle;
            foreground = GetForegroundWindow();

            if (!istop && foreground == workerw)
            {
                SetTop(this);
            }
            if (istop && (foreground != workerw && foreground != self))
            {
                SetBottom(this);
            }
            self = IntPtr.Zero;
            foreground = IntPtr.Zero;
            helper = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetBottom(this);
        }



        #endregion
    }
}
