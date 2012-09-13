namespace CarDetectorTester.Models
{
    public class ChannelStatistics : Cinch.EditableValidatingObject
    {
        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set
            {
                _channelName = value;
                NotifyPropertyChanged("ChannelName");
            }
        }


        private int _carInCount;
        public int CarInCount
        {
            get { return _carInCount; }
            set
            {
                _carInCount = value;
                NotifyPropertyChanged("CarInCount");

            }
        }


        private int _carOutCount;
        public int CarOutCount
        {
            get { return _carOutCount; }
            set
            {
                _carOutCount = value;
                NotifyPropertyChanged("CarOutCount");
            }
        }


        private bool _isCarIn;
        public bool IsCarIn
        {
            get { return _isCarIn; }
            set
            {
                _isCarIn = value;
                NotifyPropertyChanged("IsCarIn");
            }
        }

        public void Reset()
        {
            IsCarIn = false;
            CarInCount = 0;
            CarOutCount = 0;
        }
    }
}