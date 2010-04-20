using System;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common.Forms;
using Damany.Imaging.Extensions;
using System.Linq;

namespace Damany.RemoteImaging.Common.Presenters
{
    public class FaceComparePresenter
    {
        public FaceComparePresenter(FaceCompare view,
                                    IRepository repository,
                                    IRepositoryFaceComparer comparer)
        {
            this.view = view;
            this.repository = repository;
            _comparer = comparer;
            this.exit = false;

            this.Thresholds = new float[] {55, 58, 60} ;
        }

        public float ComparerSensitivity
        {
            set
            {
                _comparer.SetSensitivity(value);
            }
        }

        public void ThresholdChanged()
        {
            var acc = this.view.SelectedAccuracy;

            this.ThresholdIndex = (int) acc;

        }

        public float[] Thresholds { get; set; }

        public void CompareClicked()
        {
            if (IsRunning)
            {
                this.Exit = true;
            }
            else
            {
                this.Exit = false; 
                
                var from = this.view.SearchFrom;
                var to = this.view.SearchTo;

                var range = new Damany.Util.DateTimeRange(from, to);

                var image = this.view.Image;
                var rect = this.view.FaceRect;

                this.view.ClearFaceList();

                System.Threading.ThreadPool.QueueUserWorkItem(o =>
                    this.CompareFace(range, image, rect));

            }

         

           
        }

        public void Start()
        {
            this.view.AttachPresenter(this);
            this.ThresholdIndex = 1;
            this.view.SelectedAccuracy = CompareAccuracy.Middle;


            this.view.ShowDialog(System.Windows.Forms.Application.OpenForms[0]);
        }

        public void Stop()
        {
            this.Exit = true;
        }

        private void CompareFace(
            Damany.Util.DateTimeRange range,
            OpenCvSharp.IplImage targetImage, OpenCvSharp.CvRect rect)
        {
            try
            { 
                IsRunning = true;
                this.view.EnableStartButton(false);

                targetImage.ROI = rect;
                var portraits = this.repository.GetPortraits(range);
                int count = 0;

                var repo = new PersonOfInterest[] {new PersonOfInterest(targetImage)};
                this._comparer.Load( repo.ToList() );

                foreach (var p in portraits)
                {
                    if (Exit)
                    {
                        break;
                    }

                    this.view.CurrentImage = p.GetIpl().ToBitmap();

                    var imgFromRepository = p.GetIpl();

                    var result = this._comparer.CompareTo(imgFromRepository);

                    foreach (var repositoryCompareResult in result)
                    {
                        if (repositoryCompareResult.Similarity > Thresholds[ThresholdIndex])
                        {
                            count++;
                            this.view.AddPortrait(p);
                            this.view.SetStatusText(string.Format("检索到 {0} 个目标", count));
                        }
                        
                    }
                }

            }
            finally
            {
                IsRunning = false;
                this.view.EnableStartButton(true);
            }
            
        }

        private int ThresholdIndex
        { 
            get
            {
                lock (locker)
                {
                    return _thresholdIndex;
                }
                
            }
            set
            {
                lock (locker)
                {
                    _thresholdIndex = value;
                }
                
            }
        }


        private bool IsRunning
        {
            get
            {
                lock (runningFlagLock)
                {
                    return isRunning;
                }
            }
            set
            {
                lock (runningFlagLock)
                {
                    isRunning = value;
                }
            }
        }

        private bool Exit
        {
            get
            {
                lock (exitLock)
                {
                    return exit;
                }
            }
            set
            {
                lock (exitLock)
                {
                    exit = value;
                }
            }
        }



        FaceCompare view;

        Damany.PortraitCapturer.DAL.IRepository repository;
        private readonly IRepositoryFaceComparer _comparer;

        private object exitLock = new object();
        private volatile bool exit;

        private int _thresholdIndex;
        private object locker = new object();

        private volatile bool isRunning = false;
        private object runningFlagLock = new object();
    }
}
