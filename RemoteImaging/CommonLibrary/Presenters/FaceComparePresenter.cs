using System;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common.Forms;

namespace Damany.RemoteImaging.Common.Presenters
{
    public class FaceComparePresenter
    {
        public FaceComparePresenter(FaceCompare view,
                                    IRepository repository,
                                    IFaceComparer comparer)
        {
            this.view = view;
            this.repository = repository;
            _comparer = comparer;
            this.exit = false;
        }

        public void CompareClicked()
        {
            this.view.EnableCompareButton(false);

            try
            {
                var from = this.view.SearchFrom;
                var to = this.view.SearchTo;

                var range = new Damany.Util.DateTimeRange(from, to);

                var image = this.view.Image;
                var rect = this.view.FaceRect;

                this.view.ClearFaceList();

                System.Threading.ThreadPool.QueueUserWorkItem(o =>
                    this.CompareFace(range, image, rect));

            }
            catch (Exception)
            {
                this.view.EnableCompareButton(true);
                throw;
            }

           
        }

        public void Start()
        {
            this.view.AttachPresenter(this);
            this.view.ShowDialog(System.Windows.Forms.Application.OpenForms[0]);
        }

        public void Stop()
        {
            this.exit = true;
            
        }

        private void CompareFace(
            Damany.Util.DateTimeRange range,
            OpenCvSharp.IplImage targetImage, OpenCvSharp.CvRect rect)
        {
            try
            {
                targetImage.ROI = rect;
                var portraits = this.repository.GetPortraits(range);

                foreach (var p in portraits)
                {
                    if (exit)
                    {
                        break;
                    }

                    this.view.CurrentImage = p.GetIpl().ToBitmap();

                    var imgFromRepository = p.GetIpl();
                    imgFromRepository.ROI = p.FaceBounds;

                    var result = this._comparer.Compare(targetImage, imgFromRepository);

                    if (result.IsSimilar)
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
        private readonly IFaceComparer _comparer;
        private volatile bool exit;
    }
}
