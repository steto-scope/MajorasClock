using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleFate
{
    class MainViewModel : BaseObject
    {
        public ObservableCollection<Countdown> Countdowns
        {
            get { return Get<ObservableCollection<Countdown>>("Countdowns"); }
            set { Set("Countdowns",value); }
        }

        public MainViewModel()
        {
            Countdowns = new ObservableCollection<Countdown>();
            DurationCountdown dt = new DurationCountdown();
            dt.Name = "Test-Countdown";
            dt.EnableActions=true;
            dt.EnableNotifications = true;
            dt.Duration = new TimeSpan(0, 0,10);

            DateCountdown ddt = new DateCountdown();
            ddt.EndDate = new DateTime(2014, 3, 4, 3, 0, 0);
            ddt.Name = "Date-Count";
            ddt.EnableActions = true;
            Countdowns.Add(ddt);

            Countdowns.Add(dt);
            dt.Start();
            ddt.Start();
        }
    }
}
