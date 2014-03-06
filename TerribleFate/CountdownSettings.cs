using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleFate
{
    public class CountdownSettings : BaseObject
    {
        public CountdownSettings()
        {
            Name = "Unbenannt";
            NotifyByOverlay = true;
            NotifyBySound = true;
            SoundToPlay = AppDomain.CurrentDomain.BaseDirectory+ "sounds\\cuckoo.wav";
            NotifyBefore = new TimeSpan(0, 5, 0);
            UseDuration = true;
        }

        public string Name
        {
            get { return Get<string>("Name"); }
            set { Set("Name", value); }
        }



        public string ExecuteString
        {
            get { return Get<string>("ExecuteString"); }
            set { Set("ExecuteString", value); }
        }

        public string Description
        {
            get { return Get<string>("Description"); }
            set { Set("Description", value); }
        }

        public TimeSpan NotifyBefore
        {
            get { return Get<TimeSpan>("NotifyBefore"); }
            set { Set("NotifyBefore", value);  }
        }


        public bool NotifyBySound
        {
            get { return Get<bool>("NotifyBySound"); }
            set { Set("NotifyBySound", value); }
        }

        public bool NotifyByTray
        {
            get { return Get<bool>("NotifyByTray"); }
            set { Set("NotifyByTray", value); }
        }

        public bool NotifyByOverlay
        {
            get { return Get<bool>("NotifyByOverlay"); }
            set { Set("NotifyByOverlay", value); }
        }
        public bool NotifyByOverlayFade
        {
            get { return Get<bool>("NotifyByOverlayFade"); }
            set { Set("NotifyByOverlayFade", value); }
        }

        public string SoundToPlay
        {
            get { return Get<string>("SoundToPlay"); }
            set { Set("SoundToPlay", value); }
        }


        public bool NotifyBeforeStay
        {
            get { return Get<bool>("NotifyBeforeStay"); }
            set { Set("NotifyBeforeStay", value); }
        }

        public DateTime EndDate
        {
            get { return Get<DateTime>("EndDate"); }
            set { Set("EndDate", value); Set("UseDate", true); Set("UseDuration", false); }
        }

        public TimeSpan Duration
        {
            get { return Get<TimeSpan>("Duration"); }
            set { Set("Duration", value); Set("UseDuration", true); Set("UseDate", false); }
        }

        public bool UseDuration
        {
            get
            {
                return Get<bool>("UseDuration");
            }
            set { Set("UseDuration", value); Set("UseDate", !value);  }
        }

        public bool UseDate
        {
            get
            {
                return Get<bool>("UseDate");
            }
            set { Set("UseDate", value); Set("UseDuration", !value);  }
        }
    }
}
