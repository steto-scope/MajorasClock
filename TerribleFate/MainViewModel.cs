using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TerribleFate
{
    class MainViewModel : BaseObject
    {
        private ObservableCollection<Countdown> CountdownCollection
        {
            get { return Get<ObservableCollection<Countdown>>("Countdowns"); }
            set { Set("Countdowns",value); }
        }

        public bool ShowInactive
        {
            get { return Get<bool>("ShowInactive"); }
            set { Set("ShowInactive", value); OnPropertyChanged("Countdowns"); }
        }

        public MainViewModel()
        {
            CountdownCollection = new ObservableCollection<Countdown>();
            CountdownCollection.CollectionChanged += Countdowns_CollectionChanged;
            Countdown dt = new Countdown();
           
            dt.EnableActions=true;
            dt.EnableNotifications = true;
            CountdownSettings s = new CountdownSettings() { Duration = new TimeSpan(0, 0, 10), Name = "tEst-Countdown" };
            dt.Settings = s;
            CountdownCollection.Add(dt);

            Countdown ddt = new Countdown();
            CountdownSettings s2 = new CountdownSettings() { EndDate = new DateTime(2014, 3, 5, 3, 0, 0) };
            ddt.Settings = s2;
            s2.Name = "Date-Count";
            ddt.EnableActions = true;
            CountdownCollection.Add(ddt);

            
            dt.Start();
            ddt.Start();
        }

        void Countdowns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Countdowns");
        }

        public IEnumerable<Countdown> SortedCountdownsWithInactive
        {
            get
            {
                return CountdownCollection.OrderBy(o => o.Running).ThenBy(t => t.CurrentEndDate);
            }
        }

        public IEnumerable<Countdown> SortedCountdownsWithoutInactive
        {
            get
            {
                return CountdownCollection.Where(o => o.Running).OrderBy(t => t.CurrentEndDate);
            }
        }

        //TODO sort order: ended countdowns below
        public IEnumerable<Countdown> Countdowns
        {
            get
            {
                if (ShowInactive)
                    return SortedCountdownsWithInactive;
                return SortedCountdownsWithoutInactive;
            }
        }

        private ICommand cmdNewCountdown;

        public RelayCommand NewCountdownCommand
        {
            get
            {
                if (cmdNewCountdown == null)
                    cmdNewCountdown = new RelayCommand(p => NewCountdown(p), p => CanNewCountdown(p));
                return (RelayCommand)cmdNewCountdown;
            }
        }



        public void NewCountdown(object param)
        {
            EditWindow w = new EditWindow();
            w.ShowDialog();
            if(w.Settings !=null)
            {
                Countdown c = new Countdown();
                c.Settings = w.Settings;
                if (c.Settings.UseDate)
                    c.Start();
                CountdownCollection.Add(c);
            }
        }

        public bool CanNewCountdown(object param)
        {
            return true;
        }

        private ICommand cmdDelete;

        public RelayCommand DeleteCommand
        {
            get
            {
                if (cmdDelete == null)
                    cmdDelete = new RelayCommand(p => Delete(p), p => CanDelete(p));
                return (RelayCommand)cmdDelete;
            }
        }

        public void Delete(object param)
        {
            Countdown c = param as Countdown;
            if(c!=null)
            {
                c.Stop();
                CountdownCollection.Remove(c);
            }
        }

        public bool CanDelete(object param)
        {
            return true;
        }


    }
}
