using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using TerribleFate;

namespace MajorasClock
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr
        FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName,
        [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        static extern IntPtr GetShellWindow();

        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        */

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;




        CountdownControl cc;
        System.Windows.Media.Imaging.BitmapImage bi;
        Rectangle resolution = Screen.PrimaryScreen.Bounds;

        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        public Form1()
        {

            
            cc = new CountdownControl(Program.Args.Contains("-i"));         

            Point p = new Point((int)cc.ViewModel.Config.Left, (int)cc.ViewModel.Config.Top);
            StartPosition = FormStartPosition.Manual;
            Location = p;
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;

            cc.ViewModel.CloseRequest += ViewModel_CloseRequest;
            elementHost1.Child = cc;

            //ShowInTaskbar = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            t.Tick += t_Tick;
            t.Interval = 80;
           
            iss = new ImageBrush(bi);
            cc.MouseMove += u_MouseMove;
            cc.MouseUp += u_MouseUp;
            cc.PreviewMouseDown += cc_MouseDown;
            cc.ViewModel.SizeChanged += ViewModel_SizeChanged;

            elementHost1.AutoSize = true;
            
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
             this.AutoSize = true;
             
             SystemEvents.SessionEnding += SystemEvents_SessionEnding;
             //int initialStyle = GetWindowLong(this.Handle, -20);
            // SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            
            SystemEvents.SessionEnding+=SystemEvents_SessionEnding;
 
            DockStart();


            //SetLayeredWindowAttributes(Handle, 0, 128, LWA_COLORKEY);
        }


        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            UpdateLocation();
            cc.ViewModel.SaveCountdowns();
            cc.ViewModel.SaveConfig();
        }

        void ViewModel_SizeChanged(object sender, EventArgs e)
        {
            t.Start();
        }

        void cc_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            /*if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (!MouseIsDown)
                {
                    MouseIsDown = true;
                }
            }*/
        }

        void t_Tick(object sender, EventArgs e)
        {
           UpdateBackground();
           t.Stop();
        }


        private void UpdateBackground()
        {

            Bitmap i = Screenshot();
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(i.GetHbitmap(),
                                                                 IntPtr.Zero,
                                                                 System.Windows.Int32Rect.Empty,
                                                                 System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
                );
            iss = new ImageBrush(bitmapSource);
            cc.Background = iss;
        }

        public void UpdateLocation()
        {
            if (cc.ViewModel != null)
            {
                cc.ViewModel.Config.Left = Location.X;
                cc.ViewModel.Config.Top = Location.Y;
            }
        }

        void ViewModel_CloseRequest(object sender, EventArgs e)
        {
            UpdateLocation();
            cc.ViewModel.SaveCountdowns();
            cc.ViewModel.SaveConfig();
            Close();
        }





        void u_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           //if(MouseIsDown)
           //     t.Start(); 
            MouseIsDown = false;
            //BackgroundImageLayout = ImageLayout.Stretch;
        }

        void u_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var p = e.GetPosition(cc);
            if(!cc.ViewModel.Locked)
                Form1_MouseMove(sender, new MouseEventArgs(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed ? MouseButtons.Left : MouseButtons.None, 0, (int)p.X, (int)p.Y, 0));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DockStop();
        }


        bool MouseIsDown = false;
        Point MouseLoc = new Point();

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!MouseIsDown)
                {
                    MouseIsDown = true;
                    MouseLoc = new Point(e.X, e.Y);
                }
                Location = new Point(Location.X + e.X - MouseLoc.X, Location.Y + e.Y - MouseLoc.Y);
                UpdateLocation();
            }
        }


        
        public void DockStart()
        {
            IntPtr pWnd = FindWindow("ProgMan", null);
            //pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
            //pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);

            if(pWnd==IntPtr.Zero) //Win7 desktop rotation causes SysListView32 to be child of a WorkerW-instance instead of ProgMan
            {
                IntPtr desktop = GetDesktopWindow();
                IntPtr target = IntPtr.Zero;
                do
                {
                    pWnd = FindWindowEx(desktop, pWnd, "WorkerW", null);
                    target = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                } while (target == IntPtr.Zero && pWnd != IntPtr.Zero);

                //pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);
            }
            IntPtr tWnd = this.Handle;
            SetParent(tWnd, pWnd);

        }

        ImageBrush iss;
        
        public void DockStop()
        {
            IntPtr hwndParent = FindWindow("screenClass", null);
            SetParent(ParentForm.Handle, hwndParent);
        }


         
        Bitmap Screenshot()
        {

            

            Bitmap bmpScreenCapture = new Bitmap(Size.Width, Size.Height);

            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    ScreenCapture sc = new ScreenCapture();
                    Visible = false;
                    Bitmap i = (Bitmap)sc.CaptureWindow(GetShellWindow());
                    Visible = true;
                    g.DrawImage(i,0,0, new Rectangle(Location.X, Location.Y,Size.Width, Size.Height), GraphicsUnit.Pixel);
                    
                    
                }
            }



            return bmpScreenCapture;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (cc.ViewModel.Locked)
                UpdateBackground();
        }
    }

    public enum WallpaperType
    {
        Fill, Uniform, UniformToFill, Centered, Tiled, Color, Unknown
    }
}
