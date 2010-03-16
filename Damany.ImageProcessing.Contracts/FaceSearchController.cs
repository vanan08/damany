using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public class FaceSearchController
    {
        public void RegisterPortraitHandler( IPortraitHandler handler  )
        {
            this.PortraitFinder.AddListener(handler);
        }

        public void UnRegisterPortraitHandler(IPortraitHandler handler)
        {
            this.PortraitFinder.RemoveListener(handler);
        }

        public void Start()
        {
            this.Worker.Start();
        }

        public void Stop()
        {
            this.Worker.Stop();
        }

        public void SpeedUp()
        {
            this.Worker.WorkFrequency *= 2;
        }

        public void SlowDown()
        {
            this.Worker.WorkFrequency /= 2;
        }

        public MotionDetector MotionDetector { get; internal set; }
        public PortraitFinder PortraitFinder { get; internal set; }
        internal Damany.Util.PersistentWorker Worker { get; set; }

    }
}
