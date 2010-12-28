using System;
using MiscUtil;

namespace Damany.Imaging.Common
{
    public class NullEventAggregator : IEventAggregator
    {
        public event EventHandler<EventArgs<Portrait>> PortraitFound;
        public event EventHandler<EventArgs<PersonOfInterestDetectionResult>> FaceMatchFound;


        public void PublishPortrait(Portrait portrait)
        {
        }

        public void PublishFaceMatchEvent(PersonOfInterestDetectionResult matchResult)
        {
        }

    }
}