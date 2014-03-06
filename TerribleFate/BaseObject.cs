using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TerribleFate
{
    /// <summary>
    /// Base Class for flexible Objects
    /// </summary>
    public abstract class BaseObject : INotifyPropertyChanged
    {
        #region Privates

        private IDictionary<string, object> values = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

        #endregion

        #region Methods
        /// <summary>
        /// Gets value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default(T);

            var value = this.Get(key);

            if (value is T)
                return (T)value;

            return default(T);
        }

        private object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            if (this.values.ContainsKey(key))
                return this.values[key];

            return null;
        }
        /// <summary>
        /// Sets value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            if (!this.values.ContainsKey(key))
                this.values.Add(key, value);
            else
                this.values[key] = value;

            OnPropertyChanged(key);
            Console.WriteLine(key + ":" + value);
        }

        #endregion
        /// <summary>
        /// PropertyChanged-Event (INotifyPropertyChanged)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes PropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
