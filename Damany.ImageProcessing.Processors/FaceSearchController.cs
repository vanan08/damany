using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;

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

        internal MotionDetector MotionDetector { get; set; }
        internal PortraitFinder PortraitFinder { get; set; }
        internal Damany.Util.PersistentWorker Worker { get; set; }

    }
}
