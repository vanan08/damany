using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    [Serializable]
    public class HostConfiguration : System.ComponentModel.INotifyPropertyChanged
    {

        public HostConfiguration(object ID)
        {
            this.StationID = ID;
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private int _CameraID;
        public int CameraID
        {
            get
            {
                return _CameraID;
            }
            set
            {
                _CameraID = value;
                NotifyPropertyChanged("CameraID");
            }
        }


        public object StationID { get; private set; }

        public int TotalStorageCapacityMB { get; set; }

        public int ReservedStorageCapacityMB { get; set; }


        public void CopyTo(HostConfiguration to)
        {
            to.CameraID = this.CameraID;
            to.StationID = this.StationID;
            to.Name = this.Name;
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
