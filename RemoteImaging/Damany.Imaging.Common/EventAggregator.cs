using System;
using System.Collections.Concurrent;
using MiscUtil;

namespace Damany.Imaging.Common
{
    public class EventAggregator : IEventAggregator
    {
        public event EventHandler<EventArgs<Portrait>> PortraitFound;
        public event EventHandler<EventArgs<PersonOfInterestDetectionResult>> FaceMatchFound;


        public void PublishFaceMatchEvent(PersonOfInterestDetectionResult matchResult)
        {
            var e = new EventArgs<PersonOfInterestDetectionResult>(matchResult);
            this.InvokeFaceMatchFound(e);
        }

        public void PublishPortrait(Portrait portrait)
        {
            var e = new EventArgs<Portrait>(portrait);
            this.InvokePortraitFound(e);
        }

        private void InvokePortraitFound(EventArgs<Portrait> e)
        {
            EventHandler<EventArgs<Portrait>> handler = PortraitFound;
            if (handler != null) handler(this, e);
        }

        private void InvokeFaceMatchFound(EventArgs<PersonOfInterestDetectionResult> e)
        {
            EventHandler<EventArgs<PersonOfInterestDetectionResult>> handler = FaceMatchFound;
            if (handler != null) handler(this, e);
        }


    }
}