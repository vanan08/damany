using System;
using System.ComponentModel;

namespace CameraSearcher.Models
{
    public class Camera : INotifyPropertyChanged
    {
        public Camera()
        {
            LastSeenActive = DateTime.Now;

        }

        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                FirePropertyChanged("Ip");
            }
        }

        private DateTime _lastSeenActive;
        public DateTime LastSeenActive
        {
            get { return _lastSeenActive; }
            set
            {
                _lastSeenActive = value;
                FirePropertyChanged("LastSeenActive");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var arg = new PropertyChangedEventArgs(propertyName);
                handler(this, arg);
            }
        }
    }
}