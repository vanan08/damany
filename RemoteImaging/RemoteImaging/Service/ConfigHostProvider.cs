using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RemoteImaging.Service
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ConfigHostProvider : RemoteControlService.IConfigHost
    {
        object locker = new object();
        MotionDetectWrapper.MotionDetector motionDetector;
        FaceSearchWrapper.FaceSearch faceSearcher;
        RealtimeDisplay.Presenter presenter;

        public ConfigHostProvider(
            MotionDetectWrapper.MotionDetector detector,
            FaceSearchWrapper.FaceSearch searcher,
            RealtimeDisplay.Presenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter", "presenter is null.");
            if (detector == null)
                throw new ArgumentNullException("detector", "detector is null.");
            if (searcher == null)
                throw new ArgumentNullException("searcher", "searcher is null.");


            this.motionDetector = detector;
            this.faceSearcher = searcher;
            this.presenter = presenter;
        }

        #region IConfigHost Members

        public void SetHostName(string name)
        {
            Properties.Settings.Default.HostName = name;
        }

        public void SetForbiddenRegion(System.Drawing.Rectangle rect)
        {
            motionDetector.SetAlarmArea(
                rect.Left, 
                rect.Y, 
                rect.Left + rect.Width, 
                rect.Y + rect.Height, false);
        }

        public void UpdateBackgroundImage()
        {
            this.presenter.UpdateBG();
        }

        public void SetReservedDiskSpaceMB(int capacity)
        {
            Properties.Settings.Default.ReservedDiskSpaceMB = capacity.ToString();
        }

        #endregion
    }
}
