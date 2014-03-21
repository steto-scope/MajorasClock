using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
            mw.Load();
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
            //t.Start();
        }

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 WM_MOVE = 0x0003;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_NOZORDER = 0x0004;
        const int WM_ACTIVATEAPP = 0x001C;
        const int WM_ACTIVATE = 0x0006;
        const int WM_SETFOCUS = 0x0007;
        const int WM_WINDOWPOSCHANGED = 0x0047;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const int WM_WINDOWPOSCHANGING = 0x0046;
        bool istop = false;
        static readonly IntPtr HWND_TOP = new IntPtr(-1);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public void SetBottom(Window window)
        {
            //Topmost = false;
            //SetWindowPos(new WindowInteropHelper(this).Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
            //istop = false;
        }
        public void SetTop(Window window)
        {
            //Topmost = true;
            //SetWindowPos(new WindowInteropHelper(this).Handle, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER);
            //istop = true;
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
            //SetBottom(this);
            /*if (!istop && foreground == workerw)
            {
                SetTop(this);
            }
            if (istop && (foreground != workerw && foreground != self))
            {
                SetBottom(this);
            }*/

            if (xPos < -30000 || xPos < -30000)
                SetTop(this);
            else
                SetBottom(this);

            //self = IntPtr.Zero;
            //foreground = IntPtr.Zero;
            helper = null;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        const int GWL_HWNDPARENT = -8;
//        [DllImport("User32.dll")]
        //static extern Int32 FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll")]
        static extern int SetParent(int hWndChild, int hWndNewParent);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        
        //public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName); 
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).AddHook(WndProc);
           // Topmost = true;

            IntPtr hprog = FindWindowEx(
    FindWindowEx(
        FindWindow("Progman", "Program Manager"),
        IntPtr.Zero, "SHELLDLL_DefView", ""
    ),
    IntPtr.Zero, "SysListView32", "FolderView"
);

            SetWindowLong(new WindowInteropHelper(this).Handle, GWL_HWNDPARENT, hprog);
        }

        private int xPos;
        private int yPos;
        
        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            /*if (msg == WM_SETFOCUS)
            {
                hWnd = new WindowInteropHelper(this).Handle;
                SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
                handled = true;
            }
            if(msg == WM_WINDOWPOSCHANGING)
            {
                hWnd = new WindowInteropHelper(this).Handle;
                SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_NOZORDER);
                handled = true;
            }*/
            if(msg == WM_MOVE)
            {
                int x = unchecked((short)(long)lParam);
                int y = unchecked((short)((long)lParam >> 16));
                xPos = x;
                yPos = y;
                SetForegroundWindow(new WindowInteropHelper(this).Handle);

            }
            if(msg == WM_WINDOWPOSCHANGED)
            {
                Thread.Sleep(100);
                Activate();
                BringIntoView();
                Show();
            }
            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).RemoveHook(WndProc);
            MainViewModel mw = DataContext as MainViewModel;
            if(mw!=null)
            {
                mw.Save();
            }
        }
    }
}
