﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TerribleFate
{
    public class MainViewModel : BaseObject
    {
        private ObservableCollection<Countdown> CountdownCollection
        {
            get { return Get<ObservableCollection<Countdown>>("Countdowns"); }
            set { Set("Countdowns",value); }
        }

        public AppSettings Config
        {
            get { return Get<AppSettings>("Config"); }
            set { Set("Config", value); }
        }

        public bool ShowInactive
        {
            get { return Get<bool>("ShowInactive"); }
            set { Set("ShowInactive", value); OnPropertyChanged("Countdowns"); }
        }

        public bool IsMouseOver
        {
            get { return Get<bool>("IsMouseOver"); }
            set { Set("IsMouseOver", value);  }
        }

       

        public MainViewModel()
        {
            Config = new AppSettings();
            CountdownCollection = new ObservableCollection<Countdown>();
            CountdownCollection.CollectionChanged += Countdowns_CollectionChanged;
            
            
            /*Countdown dt = new Countdown();
           
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
            ddt.Start();*/
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

        private ICommand cmdClose;

        public RelayCommand CloseCommand
        {
            get
            {
                if (cmdClose == null)
                    cmdClose = new RelayCommand(p => Close(p), p => CanClose(p));
                return (RelayCommand)cmdClose;
            }
        }

        public void Close(object param)
        {
            if (CloseRequest != null)
                CloseRequest(null, null);
        }

        public event EventHandler CloseRequest;

        public bool CanClose(object param)
        {
            return true;
        }


        private ICommand cmdInfo;

        public RelayCommand InfoCommand
        {
            get
            {
                if (cmdInfo == null)
                    cmdInfo = new RelayCommand(p => Info(p), p => CanInfo(p));
                return (RelayCommand)cmdInfo;
            }
        }

        public void Info(object param)
        {
            AboutBox a = new AboutBox();
            a.ShowDialog();
        }

        public bool CanInfo(object param)
        {
            return true;
        }


        public void Save()
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ObservableCollection<Countdown>), new Type[] { typeof(CountdownSettings) });
                if (!Directory.Exists(App.ConfigSaveFilePath))
                    Directory.CreateDirectory(App.ConfigSaveFilePath);

                File.Delete(App.ConfigSaveFilePath);
                Stream f = File.OpenWrite(App.CountdownSaveFile);
                s.Serialize(f, CountdownCollection);
                f.Close();
            }
            catch { }

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(AppSettings));
                if (!Directory.Exists(App.ConfigSaveFilePath))
                    Directory.CreateDirectory(App.ConfigSaveFilePath);
                File.Delete(App.ConfigSaveFile);
                Stream f = File.OpenWrite(App.ConfigSaveFile);
                s.Serialize(f, Config);
                f.Close();
            }
            catch { }
        }

        public void Load()
        {
            try
            {
                if (File.Exists(App.ConfigSaveFile))
                {
                    using (Stream f = File.OpenRead(App.ConfigSaveFile))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(AppSettings));
                        var col = (AppSettings)s.Deserialize(f);
                        Config = col;

                        f.Close();
                    }
                }
            }
            catch { }
            try
            {
                if (File.Exists(App.CountdownSaveFile))
                {
                    using(Stream f = File.OpenRead(App.CountdownSaveFile))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(ObservableCollection<Countdown>), new Type[] { typeof(CountdownSettings) });
                        var col = (ObservableCollection<Countdown>)s.Deserialize(f);
                        foreach (var c in col)
                        {
                            CountdownCollection.Add(c);
                        }

                        foreach (var c in CountdownCollection)
                            if (c.RunningSerialized)
                                c.Start();

                        f.Close();
                    }
                }
            }
            catch { }
        }

    }
}
