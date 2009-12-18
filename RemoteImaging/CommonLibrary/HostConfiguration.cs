using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    [Serializable]
    public class HostConfiguration : System.ComponentModel.INotifyPropertyChanged
    {
        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;

                NotifyPropertyChanged("Index");

            }
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


        public object ID 
        {
            get
            {
                return this.Index;
            }
        }

        public void CopyTo(HostConfiguration to)
        {
            to.CameraID = this.CameraID;
            to.Index = this.Index;
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
