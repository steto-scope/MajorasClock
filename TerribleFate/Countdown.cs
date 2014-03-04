using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TerribleFate
{
    abstract class Countdown : BaseObject
    {
        /*public CountdownType Type
        {
            get { return Get<CountdownType>("Type"); }
            set { Set("Type", value); }
        }*/

        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();

        public bool Running { get; set; }

        public abstract DateTime CurrentEndDate { get; }

        public bool EnableNotifications { get; set; }
        public bool EnableActions { get; set; }

        public abstract void ExecuteActions();
        public abstract void ShowNotifications();

        public virtual bool CanReset { get { return false; } }


        public string Name
        {
            get { return Get<string>("Name"); }
            set { Set("Name", value); }
        }
        
        public bool StartStop
        {
            get
            {
                return Get<bool>("startstop");
            }
            set
            {
                Set("startstop", value);
                if (value)
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
