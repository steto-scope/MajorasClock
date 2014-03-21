using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajorasClock
{
    public partial class CountdownItem : UserControl
    {
        private Countdown countdown;
        public Countdown Countdown
        {
            get
            {
                return countdown;
            }
            set
            {
                countdown = value;
                if(value!=null)
                {
                    countdownName.Text = countdown.Settings.Name;
                    endDate.Text = countdown.CurrentEndDate.ToString();
                }
            }
        }

        


        private bool ismouseover;
        public bool IsMouseOver
        {
            get
            {
                return ismouseover;
            }
            set
            {
                ismouseover = value;
                ShowHideIcons();
            }
        }

        public void ShowHideIcons()
        {
            iconsPanel.Visible = IsMouseOver;
        }

        public void UpdateIcons()
        {
            if (Countdown.Settings.UseDuration)
            {
                playpause.Visible = true;
                playpause.Image = Countdown.Running ? Properties.Resources.pause : Properties.Resources.play;
                reset.Visible = true;
            }
            else
            {
                playpause.Visible = false;
                reset.Visible = false;
            }

            exec.Image = Countdown.EnableActions ? Properties.Resources.noactions : Properties.Resources.actions;
            notify.Image = Countdown.EnableNotifications ? Properties.Resources.nonotify : Properties.Resources.notify;
          
        }

        public CountdownItem()
        {
            InitializeComponent();
            GlobalMouseHandler gmh = new GlobalMouseHandler();
            gmh.TheMouseMoved += new MouseMovedEvent(gmh_TheMouseMoved);
            Application.AddMessageFilter(gmh);
            ShowHideIcons();
        }

        void gmh_TheMouseMoved()
        {
            Point cur_pos = System.Windows.Forms.Cursor.Position;
            cur_pos = PointToClient(cur_pos);
            //System.Console.WriteLine(cur_pos);
            if (ClientRectangle.Contains(cur_pos))
                IsMouseOver = true;
            else
                IsMouseOver = false;
        }

        public CountdownItem(Countdown c) : this()
        {
            Countdown = c;
            InfoDelegate id = new InfoDelegate(UpdateRemainingTime);
            Countdown.ProgressChangedDelegate = id;
            InfoDelegate id2 = new InfoDelegate(UpdateIcons);
            Countdown.RunningStateChangedDelegate = id2;
            UpdateIcons();
        }

        private void UpdateRemainingTime()
        {
            remainingTime.Text = Countdown.Left.ToCountdownString();
            remainingTime.Width = remainingTime.Text.Length * 10;
        }


        private void playpause_Click(object sender, EventArgs e)
        {
            Countdown.StartStop = !Countdown.StartStop;
        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void reset_Click(object sender, EventArgs e)
        {
            Countdown.Reset(null);
        }

        private void notify_Click(object sender, EventArgs e)
        {
            Countdown.EnableNotifications = !Countdown.EnableNotifications;
            UpdateIcons();
        }

        private void exec_Click(object sender, EventArgs e)
        {
            Countdown.EnableActions = !Countdown.EnableActions;
            UpdateIcons();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            
        }
    }

    public delegate void MouseMovedEvent();

    public class GlobalMouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public event MouseMovedEvent TheMouseMoved;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                if (TheMouseMoved != null)
                {
                    TheMouseMoved();
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}
