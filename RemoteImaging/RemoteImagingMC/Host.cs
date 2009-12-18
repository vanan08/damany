using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class Host : System.ComponentModel.INotifyPropertyChanged
    {
        private HostConfiguration _Config;
        public HostConfiguration Config
        {
            get
            {
                return _Config;
            }
            set
            {
                if (value == null) throw new NullReferenceException();

                if (_Config == value)
                    return;

                _Config = value;

                value.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(value_PropertyChanged);

                NotifyPropertyChanged("Config");
            }
        }

        void value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged("Config");
        }


        private DateTime _LastSeen;
        public DateTime LastSeen
        {
            get
            {
                return _LastSeen;
            }
            set
            {
                _LastSeen = value;
                NotifyPropertyChanged("LastSeen");
            }
        }

        private HostStatus _Status;
        public HostStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                NotifyPropertyChanged("Status");
            }
        }
        private System.Net.IPAddress _Ip;
        public System.Net.IPAddress Ip
        {
            get
            {
                return _Ip;
            }
            set
            {
                _Ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Host)) return false;

            return this.Config.ID.Equals((obj as Host).Config.ID);
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }



        public void CopyTo(Host to)
        {
            to.Config = this.Config;
            to.Ip = this.Ip;
            to.LastSeen = this.LastSeen;
            to.Status = this.Status;
        }


        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
