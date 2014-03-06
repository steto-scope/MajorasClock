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
            SoundToPlay = AppDomain.CurrentDomain.BaseDirectory+ "sounds\\alarm.mp3";
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
            get { return Get<string>("execstring"); }
            set { Set("execstring", value);}
        }

        public TimeSpan NotifyBefore
        {
            get { return Get<TimeSpan>("NotifyBefore"); }
            set { Set("NotifyBefore", value);  }
        }


        public bool NotifyBySound
        {
            get { return Get<bool>("notifybysound"); }
            set { Set("notifybysound", value);  }
        }

        public bool NotifyByTray
        {
            get { return Get<bool>("notifybytray"); }
            set { Set("notifybytray", value); }
        }

        public bool NotifyByOverlay
        {
            get { return Get<bool>("notifybyoverlay"); }
            set { Set("notifybyoverlay", value); }
        }
        public bool NotifyByOverlayFade
        {
            get { return Get<bool>("notifybyoverlayfade"); }
            set { Set("notifybyoverlayfade", value); }
        }

        public string SoundToPlay
        {
            get { return Get<string>("soundtoplay"); }
            set { Set("soundtoplay", value); }
        }


        public bool NotifyBeforeStay
        {
            get { return Get<bool>("notifybeforestay"); }
            set { Set("notifybeforestay", value); }
        }

        public DateTime EndDate
        {
            get { return Get<DateTime>("EndDate"); }
            set { Set("EndDate", value); Set("UseDate", true); Set("UseDuration", false); }
        }

        public TimeSpan Duration
        {
            get { return Get<TimeSpan>("duration"); }
            set { Set("duration", value); Set("UseDuration", true); Set("UseDate", false); }
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
