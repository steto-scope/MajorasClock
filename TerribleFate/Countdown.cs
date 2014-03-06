using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TerribleFate
{
    class Countdown : BaseObject
    {
        /*public CountdownType Type
        {
            get { return Get<CountdownType>("Type"); }
            set { Set("Type", value); }
        }*/

        public Countdown()
        {
            EnableNotifications = true;
            EnableActions = true;
            Running = false;
        }

        public CountdownSettings Settings
        {
            get { return Get<CountdownSettings>("Settings"); }
            set { Set("Settings", value); OnPropertyChanged("CurrentEndDate"); OnPropertyChanged("Name"); if (value.UseDate) Set("Running", true); }
        }

        public TimeSpan Left
        {
            get {
                if(Settings.UseDuration)
                    return TimeSpan.FromSeconds(Settings.Duration.TotalSeconds - Elapsed); 
                else
                {
                    if ((Settings.EndDate - DateTime.Now).TotalSeconds < 0)
                        return new TimeSpan();
                    return Settings.EndDate - DateTime.Now; 
                }
            }

        }

        public DateTime CurrentEndDate
        {
            get {
                if (Settings.UseDate)
                    return Settings.EndDate;
                return DateTime.Now.Add(new TimeSpan(0, 0, (int)(Settings.Duration.TotalSeconds - Elapsed))); }
        }

        public long Elapsed
        {
            get { return Get<long>("Elapsed"); }
            private set { Set("Elapsed", value); OnPropertyChanged("Left"); }
        }

        Task t;
        CancellationTokenSource ct = new CancellationTokenSource();

        public void Start()
        {
            ct = new CancellationTokenSource();
            if(Settings.UseDuration)
                t = Task.Run(() => CountDuration(ct.Token), ct.Token);
            else
                t = Task.Run(() => CountDate(ct.Token), ct.Token);
        }

        async Task CountDuration(CancellationToken tok)
        {
            bool wasinloop = false;
            Running = true;
            while (Elapsed < Settings.Duration.TotalSeconds)
            {
                wasinloop = true;
                Elapsed++;
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
            }
            if (wasinloop)
            {
                if (!(Elapsed < Settings.Duration.TotalSeconds) && EnableNotifications)
                    ShowNotifications();
                if (!(Elapsed < Settings.Duration.TotalSeconds) && EnableActions)
                    ExecuteActions();
            }
            Running = false;
        }

        async Task CountDate(CancellationToken tok)
        {
            bool wasinloop = false;
            Running = true;
            while (DateTime.Now < Settings.EndDate)
            {
                wasinloop = true;
                OnPropertyChanged("Left");
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
            }
            if (wasinloop)
            {
                if (!(DateTime.Now < Settings.EndDate) && EnableNotifications)
                    ShowNotifications();
                if (!(DateTime.Now < Settings.EndDate) && EnableActions)
                    ExecuteActions();
            }
            Running = false;
        }

        public void Stop()
        {
            ct.Cancel();
        }

        public void ResetCountdown()
        {
            if (Settings.UseDuration)
            {
                Elapsed = 0;
            }
        }
        public bool Running
        {
            get
            {
                return Get<bool>("Running");
            }
            set
            {
                Set("Running", value);
                Set("NotRunning", !value);
                OnPropertyChanged("StartStop");
            }
        }

        public bool NotRunning
        {
            get { return Get<bool>("NotRunning"); }
        }

        public bool EnableNotifications
        {
            get { return Get<bool>("EnableNotifications"); }
            set { Set("EnableNotifications", value);  }
        }
        public bool EnableActions
        {
            get { return Get<bool>("EnableActions"); }
            set { Set("EnableActions", value); }
        }

        public void ExecuteActions()
        {
            if (!EnableActions)
                return;

            if (Settings.ExecuteString != null)
                Process.Start(Settings.ExecuteString);
        }
        public void ShowNotifications()
        {
            if (!EnableNotifications)
                return;

            if(Settings.NotifyBySound && Settings.SoundToPlay!=null)
            {
                if(File.Exists(Settings.SoundToPlay))
                {
                    SoundPlayer player = new SoundPlayer(Settings.SoundToPlay);
                    player.Play();
                }
            }
        }

        private ICommand cmdReset;

        public RelayCommand ResetCommand
        {
            get
            {
                if (cmdReset == null)
                    cmdReset = new RelayCommand(p => Reset(p), p => CanReset(p));
                return (RelayCommand)cmdReset;
            }
        }

        public void Reset(object param)
        {
            ResetCountdown();
        }

        public bool CanReset(object param)
        {
            return Settings.UseDuration;
        }


        private ICommand cmdEdit;

        public RelayCommand EditCommand
        {
            get
            {
                if (cmdEdit == null)
                    cmdEdit = new RelayCommand(p => Edit(p), p => CanEdit(p));
                return (RelayCommand)cmdEdit;
            }
        }

        public void Edit(object param)
        {

            EditWindow w = new EditWindow(Settings);
            w.ShowDialog();
            CountdownSettings s = w.Settings;
            if(s!=null)
            {
                Settings = s;
                if (Settings.UseDate)
                    Start();
            }
        }

        public bool CanEdit(object param)
        {
            return true;
        }


        
        
        public bool StartStop
        {
            get
            {
                return Running;
            }
            set
            {
                if (Running)
                    Stop();
                else
                    Start();
            }
        }

    }

    /*
    enum CountdownType
    {
        ToDate, Duration
    }*/
}
