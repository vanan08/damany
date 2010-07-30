using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video;
using Damany.Cameras;
using Microsoft.Practices.Composite.Events;

namespace StockTraderRI.Modules
{
    class FrameCapturer
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IVideoSource _videoSource;


        public FrameCapturer(IEventAggregator eventAggregator, AForge.Video.IVideoSource videoSource)
        {
            _eventAggregator = eventAggregator;
            _videoSource = videoSource;

            _videoSource.NewFrame += _videoSource_NewFrame;
        }


        public void Start()
        {
            _videoSource.Start();
        }

        void _videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            _eventAggregator.GetEvent<NewFrameEvent>().Publish(eventArgs.Frame);
            eventArgs.Frame.Dispose();
        }
    }
}
