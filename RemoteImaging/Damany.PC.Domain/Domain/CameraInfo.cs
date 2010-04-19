using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Damany.PC.Domain
{
    public class CameraInfo : System.ComponentModel.INotifyPropertyChanged
    {
        [DisplayName("编号")]
        public int Id { get; set; }

        [DisplayName("摄像头地址")]
        public Uri Location { get; set; }


        private string _name;

    
        [DisplayName("名称")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                var e = new PropertyChangedEventArgs("Name");
                InvokePropertyChanged(e);
                
            }
        }

        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("类型")]
        public CameraProvider Provider { get; set; }

        [Browsable(false)]
        public bool Enabled { get; set; }

        [DisplayName("用户名")]
        public string LoginUserName { get; set; }

        [DisplayName("密码")]
        public string LoginPassword { get; set; }

        [DisplayName("人脸比对")]
        public bool FaceCompareEnabled { get; set; }

        public override string ToString()
        {
            var txt = string.Format("{0}", Name);
            return txt;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}
