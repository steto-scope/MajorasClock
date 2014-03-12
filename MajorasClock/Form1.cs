using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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




        CountdownControl cc;
        System.Windows.Media.Imaging.BitmapImage bi;
        Rectangle resolution = Screen.PrimaryScreen.Bounds;
        Tuple<object, WallpaperType> currentWallpaper;

        public Form1()
        {
            currentWallpaper = ReadWallpaperSettings();
            if(currentWallpaper!=null && currentWallpaper.Item2!= WallpaperType.Color)
            {
                bi = new System.Windows.Media.Imaging.BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri((string)currentWallpaper.Item1, UriKind.RelativeOrAbsolute);
                bi.EndInit();
            }
            
            cc = new CountdownControl();
            Point p = new Point((int)cc.ViewModel.Config.Left, (int)cc.ViewModel.Config.Top);
            StartPosition = FormStartPosition.Manual;
            Location = p;
            InitializeComponent();
            

            cc.ViewModel.CloseRequest += ViewModel_CloseRequest;
            elementHost1.Child = cc;

            ShowInTaskbar = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
           
            iss = new ImageBrush(bi);
            cc.MouseMove += u_MouseMove;
            cc.MouseUp += u_MouseUp;
            cc.SizeChanged+=cc_SizeChanged;
            this.Move += Form1_Move;

            
            DockStart();
        }




        void cc_SizeChanged(object sender, EventArgs e)
        {
            if (!(double.IsNaN(cc.ActualWidth) && double.IsNaN(cc.ActualHeight)))
            {
                Width = (int)cc.ActualWidth;
                Height = (int)cc.ActualHeight;
            }
        }

        void ViewModel_CloseRequest(object sender, EventArgs e)
        {
            if (cc.ViewModel != null)
            {
                cc.ViewModel.Config.Left = Location.X;
                cc.ViewModel.Config.Top = Location.Y;
                cc.ViewModel.Save();
            }
            Close();
        }





        void u_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseIsDown = false;
        }

        void u_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var p = e.GetPosition(cc);
            if(!cc.ViewModel.Config.Locked)
                Form1_MouseMove(sender, new MouseEventArgs(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed ? MouseButtons.Left : MouseButtons.None, 0, (int)p.X, (int)p.Y, 0));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DockStop();
        }


        public Tuple<object, WallpaperType> ReadWallpaperSettings()
        {

            RegistryKey k = Registry.CurrentUser;
            if (k != null)
            {
                RegistryKey desktop = k.OpenSubKey("Control Panel\\Desktop");
                if (desktop != null)
                {
                    string wallpaper = (string)desktop.GetValue("Wallpaper");
                    if(string.IsNullOrEmpty(wallpaper))
                    {
                        RegistryKey color = k.OpenSubKey("Control Panel\\Colors");
                        string[] c = ((string)color.GetValue("Background")).Split(' ');
                        var z = System.Windows.Media.Color.FromRgb(byte.Parse(c[0]) , byte.Parse(c[1]),byte.Parse(c[2]));
                        return new Tuple<object, WallpaperType>(z, WallpaperType.Color);
                    }
                    int style = int.Parse(desktop.GetValue("WallpaperStyle").ToString());
                    int tile = int.Parse(desktop.GetValue("TileWallpaper").ToString());
                    WallpaperType type = WallpaperType.Unknown;

                    if (tile == 0 && style == 10)
                        type = WallpaperType.Fill;
                    if (tile == 0 && style == 6)
                        type = WallpaperType.Uniform;
                    if (tile == 0 && style == 2)
                        type = WallpaperType.UniformToFill;
                    if (tile == 1)
                        type = WallpaperType.Tiled;

                    if (type == WallpaperType.Unknown || string.IsNullOrEmpty(wallpaper))
                        return null;

                    return new Tuple<object, WallpaperType>(wallpaper, type);
                }
            }
            return null;
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
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }

        
        public void DockStart()
        {
            IntPtr pWnd = FindWindow("ProgMan", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);

            if(pWnd==IntPtr.Zero) //Win7 desktop rotation causes SysListView32 to be child of a WorkerW-instance instead of ProgMan
            {
                IntPtr desktop = GetDesktopWindow();
                IntPtr target = IntPtr.Zero;
                do
                {
                    pWnd = FindWindowEx(desktop, pWnd, "WorkerW", null);
                    target = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                } while (target == IntPtr.Zero && pWnd != IntPtr.Zero);

                pWnd = FindWindowEx(target, IntPtr.Zero, "SysListView32", null);
            }
            IntPtr tWnd = this.Handle;
            SetParent(tWnd, pWnd);

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
        ImageBrush iss;
        void Form1_Move(object sender, EventArgs e)
        {
            if (currentWallpaper == null)
                return;

            if(currentWallpaper.Item2 == WallpaperType.Color)
            {
                SolidColorBrush scb = new SolidColorBrush((System.Windows.Media.Color)currentWallpaper.Item1);
                cc.Background = scb;
                elementHost1.Child = cc;
            }

            if(currentWallpaper.Item2 == WallpaperType.Fill)
            {
                System.Windows.Rect t = new System.Windows.Rect((double)Location.X / (double)resolution.Width, (double)Location.Y / (double)resolution.Height, (double)Width / (double)resolution.Width, (double)Height / (double)resolution.Height);
                iss.Viewbox = t;
                cc.Background = iss;
                elementHost1.Child = cc;
               
            }

            if(currentWallpaper.Item2 == WallpaperType.Tiled)
            {
                ImageBrush iss = new ImageBrush(bi);
                iss.TileMode = TileMode.Tile;

                double xfac = 1 / (bi.Width / resolution.Width);
                double yfac = 1 / (bi.Height / resolution.Height);
                double xxfac = (double)bi.Width / (double)Width;
                double yyfac = (double)bi.Height / (double)Height;

                iss.Viewport = new System.Windows.Rect(((double)Location.X / (double)resolution.Width) * -xfac * xxfac, ((double)Location.Y / (double)resolution.Height) * -yfac * yyfac, xxfac, yyfac);
                cc.Background = iss;
                elementHost1.Child = cc;
              
            }
        }
        public void DockStop()
        {
            IntPtr hwndParent = FindWindow("screenClass", null);
            SetParent(ParentForm.Handle, hwndParent);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Form1_Move(null, null);
        }

    }

    public enum WallpaperType
    {
        Fill, Uniform, UniformToFill, Centered, Tiled, Color, Unknown
    }
}
