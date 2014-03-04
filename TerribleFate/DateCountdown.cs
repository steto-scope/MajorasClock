using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TerribleFate
{
    class DateCountdown : Countdown
    {
        public DateTime EndDate
        {
            get { return Get<DateTime>("EndDate"); }
            set { Set("EndDate", value); }
        }

        public TimeSpan Left
        {
            get { return EndDate - DateTime.Now; }
        }

        async Task Count(CancellationToken tok)
        {
            Running = true;
            while (DateTime.Now < EndDate)
            {
                OnPropertyChanged("Left");
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
            }
            if (!(DateTime.Now < EndDate) && EnableNotifications)
                ShowNotifications();
            if (!(DateTime.Now < EndDate) && EnableActions)
                ExecuteActions();

            Running = false;
        }

        public DateCountdown()
        {
             
        }

        public override DateTime CurrentEndDate
        {
            get { return EndDate; }
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
