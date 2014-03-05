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
            Countdown dt = new Countdown();
           
            dt.EnableActions=true;
            dt.EnableNotifications = true;
            CountdownSettings s = new CountdownSettings() { Duration = new TimeSpan(0, 0, 10), IsDurationCountdown = true, Name = "tEst-Countdown" };
            dt.Settings = s;
            Countdowns.Add(dt);

            Countdown ddt = new Countdown();
            CountdownSettings s2 = new CountdownSettings() { EndDate = new DateTime(2014, 3, 5, 3, 0, 0) };
            ddt.Settings = s2;
            s2.Name = "Date-Count";
            ddt.EnableActions = true;
            Countdowns.Add(ddt);

            
            dt.Start();
            ddt.Start();
        }
    }
}
