using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    public class FaceComparePresenter
    {
        public FaceComparePresenter(FaceCompare view,
            Damany.PortraitCapturer.DAL.IRepository repository)
        {
            this.view = view;
            this.repository = repository;

        }

        public void CompareClicked()
        {
            this.view.EnableCompareButton(false);
            var from = this.view.SearchFrom;
            var to = this.view.SearchTo;

            var range = new Damany.Util.DateTimeRange(from, to);

            var image = this.view.Image;
            var rect = this.view.FaceRect;

            this.view.ClearFaceList();

            System.Threading.ThreadPool.QueueUserWorkItem(o =>
                this.CompareFace(range, image, rect));
        }

        public void Start()
        {
            this.view.AttachPresenter(this);
        }

        private void CompareFace(
            Damany.Util.DateTimeRange range,
            OpenCvSharp.IplImage image, OpenCvSharp.CvRect rect)
        {
            try
            {
                var portraits = this.repository.GetPortraits(range);

                foreach (var p in portraits)
                {
                    this.view.CurrentImage = p.GetIpl().ToBitmap();

                    float similarity = 0;
                    bool isSimilar = FaceProcessingWrapper.StaticFunctions.CompareFace(
                        image, rect,
                        p.GetIpl(), p.FaceBounds, ref similarity, false);

                    if (isSimilar)
                    {
                        this.view.AddPortrait(p);
                    }

                }

            }
            finally
            {
                this.view.EnableCompareButton(true);
            }
            
        }



        FaceCompare view;
        Damany.PortraitCapturer.DAL.IRepository repository;

    }
}
