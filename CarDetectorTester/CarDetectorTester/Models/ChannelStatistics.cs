namespace CarDetectorTester.Models
{
    public class ChannelStatistics : Caliburn.Micro.PropertyChangedBase
    {
        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set
            {
                _channelName = value;
                NotifyOfPropertyChange(()=>ChannelName);
            }
        }


        private int _carInCount;
        public int CarInCount
        {
            get { return _carInCount; }
            set
            {
                _carInCount = value;
                NotifyOfPropertyChange(()=>CarInCount);
            }
        }


        private int _carOutCount;
        public int CarOutCount
        {
            get { return _carOutCount; }
            set
            {
                _carOutCount = value;
                NotifyOfPropertyChange(()=>CarOutCount);
            }
        }


        private bool _isCarIn;
        public bool IsCarIn
        {
            get { return _isCarIn; }
            set
            {
                _isCarIn = value;
                NotifyOfPropertyChange(()=>IsCarIn);
            }
        }
    }
}