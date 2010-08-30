using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaceSearchWrapper;
using OpenCvSharp;

namespace FaceSearcherTester
{
    public class FaceSearchController : System.ComponentModel.INotifyPropertyChanged
    {
        private FaceSearch _searcher = new FaceSearch();

        private readonly FaceSearchConfiguration _faceSearchConfiguration = new FaceSearchConfiguration();

        private int _minFaceWidth;
        public int MinFaceWidth
        {
            get { return _minFaceWidth; }
            set
            {
                _minFaceWidth = value;
                UpdateFaceWidth();
                SearchFace();
            }
        }

        private int _maxFaceWidth;
        public int MaxFaceWidth
        {
            get { return _maxFaceWidth; }
            set
            {
                _maxFaceWidth = value;
                UpdateFaceWidth();
                SearchFace();
            }
        }

        private bool _drawFaceSize = true;
        public bool DrawFaceSize
        {
            get { return _drawFaceSize; }
            set
            {
                _drawFaceSize = value;
                RaisePropertyChanged("DrawFaceSize");
                SearchFace();
            }
        }

        private int _faceCount;
        public int FaceCount
        {
            get { return _faceCount; }
            set
            {
                _faceCount = value;
                RaisePropertyChanged("FaceCount");
            }
        }

        private Image _imageToSearch;
        public Image ImageToSearch
        {
            get { return _imageToSearch; }
            set
            {
                _imageToSearch = value;
                this.SearchFace();
            }
        }


        private Image _resultImage;
        public System.Drawing.Image ResultImage
        {
            get { return _resultImage; }
            set
            {
                _resultImage = value;
                RaisePropertyChanged("ResultImage");
            }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



        public FaceSearchController()
        {
            _minFaceWidth = 50;
            _maxFaceWidth = 300;

            _faceSearchConfiguration.SearchRectangle = new Rectangle(0, 0, 0, 0);

        }

        private void UpdateFaceWidth()
        {
            _faceSearchConfiguration.MinFaceWidth = MinFaceWidth;
            var factor = (float)MaxFaceWidth / MinFaceWidth;
            _faceSearchConfiguration.FaceWidthRatio = factor;

            _searcher.Configuration = _faceSearchConfiguration;
        }

        private void SearchFace()
        {
            if (this.ImageToSearch == null)
            {
                return;
            }

            var img = (Image)this.ImageToSearch.Clone();

            var t = Task.Factory.StartNew(() =>
                                              {

                                                  var ipl = OpenCvSharp.IplImage.FromBitmap((Bitmap)img);
                                                  var f = new Damany.Imaging.Common.Frame(ipl);
                                                  f.MotionRectangles.Add(new CvRect(0, 0, ipl.Width, ipl.Height));
                                                  _searcher.AddInFrame(f);
                                                  var result = _searcher.SearchFaces();
                                                  return result;
                                              });

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            t.ContinueWith(result =>
            {
                FaceCount = 0;
                var targets = t.Result;
                using (var bkground = Graphics.FromImage(img))
                using (var pen = new Pen(Color.Red, 3))
                using (var font = new Font("Tahoma", 20))
                {

                    foreach (var target in targets)
                    {
                        var rects = from r in target.Portraits
                                    select new Rectangle(
                                        r.FacesRectForCompare.X,
                                        r.FacesRectForCompare.Y,
                                        r.FacesRectForCompare.Width,
                                        r.FacesRectForCompare.Height);
                        foreach (var rectangle in rects)
                        {
                            bkground.DrawRectangle(pen, rectangle);

                            if (DrawFaceSize)
                            {
                                var s = string.Format("{0}X{1}", rectangle.Width, rectangle.Height);
                                bkground.DrawString(s, font, Brushes.Red, rectangle);
                            }

                            FaceCount++;
                        }

                    }

                    if (targets.Length == 0)
                    {
                        bkground.DrawRectangle(pen, 0, 0, this.ImageToSearch.Width, this.ImageToSearch.Height);
                        bkground.DrawString("未搜索到人像", font, Brushes.Red, 0, 0);
                    }
                }

                ResultImage = img;

            }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);

            t.ContinueWith(result =>
            {
                MessageBox.Show("发生异常\r\n" + result.Exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, scheduler);
        }

    }
}
