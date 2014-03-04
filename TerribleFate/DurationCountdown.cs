using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TerribleFate
{
    class DurationCountdown : Countdown
    {
        public TimeSpan Duration
        {
            get { return Get<TimeSpan>("Duration"); }
            set { Set("Duration", value); }
        }

        public long Elapsed
        {
            get { return Get<long>("Elapsed"); }
            private set { Set("Elapsed", value); OnPropertyChanged("Left"); }
        }

        public override DateTime CurrentEndDate
        {
            get { return DateTime.Now.Add(new TimeSpan(0,0,(int)(Duration.TotalSeconds-Elapsed))); }
        }

        public TimeSpan Left
        {
            get { return TimeSpan.FromSeconds(Duration.TotalSeconds - Elapsed); }
        }

        public override bool CanReset
        {
            get
            {
                return true;
            }
        }


        async Task Count(CancellationToken tok)
        {
            Running = true;
            while (Elapsed < Duration.TotalSeconds)
            {
                Elapsed++;
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
            }
            if (!(Elapsed < Duration.TotalSeconds) && EnableNotifications)
                ShowNotifications();
            if (!(Elapsed < Duration.TotalSeconds) && EnableActions)
                ExecuteActions();

            Running = false;
        }

        public DurationCountdown()
        {
            
        }

        Task t;
        CancellationTokenSource ct = new CancellationTokenSource();

        public override void Start()
        {
            ct = new CancellationTokenSource();
            t = Task.Run(() => Count(ct.Token), ct.Token);
        }

        public override void Stop()
        {
            ct.Cancel();
        }

        public override void Reset()
        {
            Stop();
            Elapsed = 0;
        }

        public override void ExecuteActions()
        {
            throw new NotImplementedException();
        }

        public override void ShowNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
