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
        //MotionDetectWrapper.MotionDetector motionDetector;
        FaceSearchWrapper.FaceSearch faceSearcher;

        public ConfigHostProvider(
            //MotionDetectWrapper.MotionDetector detector,
            FaceSearchWrapper.FaceSearch searcher
            )
        {


            //this.motionDetector = detector;
            this.faceSearcher = searcher;
            
        }

        #region IConfigHost Members

        public void SetHostName(string name)
        {
            Properties.Settings.Default.HostName = name;
        }

        public void SetForbiddenRegion(System.Drawing.Rectangle rect)
        {
//             motionDetector.SetAlarmArea(
//                 rect.Left, 
//                 rect.Y, 
//                 rect.Left + rect.Width, 
//                 rect.Y + rect.Height, false);
        }

        public void UpdateBackgroundImage()
        {
            
        }

        public void SetReservedDiskSpaceMB(int capacity)
        {
            Properties.Settings.Default.ReservedDiskSpaceMB = capacity.ToString();
        }

        #endregion
    }
}
