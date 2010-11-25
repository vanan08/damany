using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinch;

namespace CarDetectorTester.Models
{
    public class SerialportConfig : ValidatingObject
    {

        private static RegexRule portNameRule;


        public SerialportConfig()
        {
            BaundRate = 9600;
            PortName = "COM1";

            this.AddRule(portNameRule);
        }

        static SerialportConfig()
        {
            portNameRule = new RegexRule("PortName", "串口必须为'COMx'的格式", @"^com\d+$");
        }

        private int _baundRate;
        public int BaundRate
        {
            get { return _baundRate; }
            set
            {
                _baundRate = value;
                NotifyPropertyChanged("BaundRate");
            }
        }

        private string _portName;
        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value; 
                NotifyPropertyChanged("PortName");
            }
        }

    }
}
