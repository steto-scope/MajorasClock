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
using System.Xml.Serialization;

namespace TerribleFate
{
    public class Countdown : BaseObject
    {

        public Countdown()
        {
            EnableNotifications = true;
            EnableActions = true;
            Running = false;
        }

        public CountdownSettings Settings
        {
            get { return Get<CountdownSettings>("Settings"); }
            set { Set("Settings", value); OnPropertyChanged("CurrentEndDate"); OnPropertyChanged("Name"); if (value.UseDate) Start(); }
        }

        public bool IsMouseOver
        {
            get { return Get<bool>("IsMouseOver"); }
            set { Set("IsMouseOver", value); }
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
            set { Set("Elapsed", value); OnPropertyChanged("Left"); }
        }

        Task t;
        CancellationTokenSource ct = new CancellationTokenSource();

        public void Start()
        {
            if (NotRunning)
            {

                if (Settings.UseDate)
                    if (!(Elapsed < (Settings.EndDate-DateTime.Now).TotalSeconds))
                     return; 
                if (Settings.UseDuration)
                    if (!(Elapsed < Settings.Duration.TotalSeconds))
                        ResetCountdown();

                ct = new CancellationTokenSource();
                var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                if (Settings.UseDuration)
                    t = Task.Run(() => CountDuration(ct.Token), ct.Token).ContinueWith(r => Notify(), scheduler);
                else
                    t = Task.Run(() => CountDate(ct.Token), ct.Token).ContinueWith(r => Notify(), scheduler);
                runningserialized = false;
            }
        }

        public void Notify()
        {
            if (Settings.UseDuration)
            {
                if (!(Elapsed < Settings.Duration.TotalSeconds) && EnableNotifications)
                    ShowNotifications();
                if (!(Elapsed < Settings.Duration.TotalSeconds) && EnableActions)
                    ExecuteActions();
            }
            if(Settings.UseDate)
            {
                if (!(DateTime.Now < Settings.EndDate) && EnableNotifications)
                    ShowNotifications();
                if (!(DateTime.Now < Settings.EndDate) && EnableActions)
                    ExecuteActions();
            }
        }

        async Task CountDuration(CancellationToken tok)
        {
            Running = true;
            while (Elapsed < Settings.Duration.TotalSeconds)
            {
                Elapsed++;
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
            }
            Running = false;
        }

        async Task CountDate(CancellationToken tok)
        {
            Running = true;
            while (DateTime.Now < Settings.EndDate)
            {
                OnPropertyChanged("Left");
                if (tok.IsCancellationRequested)
                    break;
                await Task.Delay(1000);
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

        [XmlIgnore]
        public bool Running
        {
            get
            {
                return Get<bool>("Running");
            }
            private set
            {
                Set("Running", value);
                OnPropertyChanged("StartStop");
                OnPropertyChanged("NotRunning");
            }
        }

        private bool runningserialized;
        [XmlElement("Running")]
        public bool RunningSerialized
        {
            get { return runningserialized || Running; }
            set { runningserialized = value; }
        }

        [XmlIgnore]
        public bool RunAfterDeserialize
        {
            get { return runningserialized; }
        }

        [XmlIgnore]
        public bool NotRunning
        {
            get { return !Get<bool>("Running"); }
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
                    player = new SoundPlayer(Settings.SoundToPlay);
                    player.Play();
                }
            }

            if(Settings.NotifyByOverlay)
            {
                Process.Start("notifier.exe", "-message \""+this.Settings.Name+"\"");
                //Overlay o = new TerribleFate.Overlay(this);
                //o.Show();
            }
        }

        SoundPlayer player;

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

        public void StopSound()
        {
            if (player != null)
                player.Stop();
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


        
        [XmlIgnore]
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

}
