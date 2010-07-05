using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common;
using Damany.Util;
using RemoteControlService;
using Video = RemoteImaging.Core.Video;
using Damany.Util.Extensions;

namespace RemoteImaging.Query
{
    public class VideoQueryPresenter : IVideoQueryPresenter
    {
        private readonly IVideoQueryScreen _screen;
        private readonly IRepository _portraitRepository;
        private readonly ConfigurationManager _manager;
        private Damany.PC.Domain.CameraInfo _selectedCamera;
        private DateTimeRange _range;
        private SearchScope _scope;
        private DateTimeRange _currentRange;

        public VideoQueryPresenter(IVideoQueryScreen screen,
                                   IRepository portraitRepository,
                                   ConfigurationManager manager)
        {
            _screen = screen;
            _portraitRepository = portraitRepository;
            _manager = manager;
        }

        public void Start()
        {
            _screen.AttachPresenter(this);
            _screen.Cameras = _manager.GetCameras().ToArray();
            _screen.Show();

        }


        public void Search()
        {

            var selectedCamera = this._screen.SelectedCamera;
            if (selectedCamera == null)
            {
                return;
            }

            _selectedCamera = this._screen.SelectedCamera;
            _range = this._screen.TimeRange;
            _scope = this._screen.SearchScope;

            _currentRange = new DateTimeRange(_range.From, _range.From.AddHours(1));

            SearchAsync();
                                                             
        }

        private void SearchAsync()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o => DoSearch());
        }
        public void PlayVideo()
        {
            var p = this._screen.SelectedVideoFile;
            if (p == null) return;
            if (string.IsNullOrEmpty(p.Path))
                return;
            if (!System.IO.File.Exists(p.Path))
                return;

            this._screen.PlayVideoInPlace(p.Path);
        }

        public void ShowRelatedFaces()
        {
            var video = _screen.SelectedVideoFile;

            var from = Damany.Util.Extensions.MiscHelper.RoundToMinute(video.CapturedAt);
            var to = from.AddMinutes(1);

            var range = new DateTimeRange(from, to);

            var p = _portraitRepository.GetPortraits(_screen.SelectedCamera.Id, range);
            _screen.ClearFacesList();

            foreach (var portrait in p)
            {
                 _screen.AddFace(portrait);
            }
           
        }

        public void NextPage()
        {
            if (_selectedCamera == null)
                return;

            _currentRange.From.AddHours(1);
            _currentRange.To.AddHours(1);
        }

        public void PreviousPage()
        {
            if (_selectedCamera == null)
                return;

            _currentRange.From.AddHours(-1);
            _currentRange.To.AddHours(-1);
        }

        public void FirstPage()
        {
            if (_selectedCamera == null)
                return;
        }

        public void LastPage()
        {
            if (_selectedCamera == null)
                return;
        }

        private void DoSearch()
        {
            _screen.Busy = true;

            var videos =
                new FileSystemStorage(
                    Properties.Settings.Default.OutputPath).
                    VideoFilesBetween(_selectedCamera.Id,
                                      _currentRange.From, _currentRange.To);

            if (videos.Count() < 0)
            {
                return;
            }

            var frameQuery = _portraitRepository.GetFramesQuery()
                .Where(
                    frame =>
                    frame.CapturedFrom.Id == _selectedCamera.Id
                    && frame.CapturedAt >= _currentRange.From &&
                    frame.CapturedAt <= _currentRange.To);

            var frameHash = new HashSet<DateTime>();
            foreach (var g in frameQuery)
            {
                var round = g.CapturedAt.RoundToMinute();
                frameHash.Add(round);
            }


            var portraitQuery =
                _portraitRepository.GetPortraits(
                    _selectedCamera.Id, _currentRange).ToArray();
            var portraitHash = new HashSet<DateTime>();
            foreach (var portrait in portraitQuery)
            {
                var round = portrait.CapturedAt.RoundToMinute();
                portraitHash.Add(round);
            }


            this._screen.ClearAll();

            foreach (var v in videos)
            {
                v.HasMotionDetected =
                    frameHash.Contains(v.CapturedAt);
                v.HasFaceCaptured =
                    portraitHash.Contains(v.CapturedAt);


                if ((_scope & SearchScope.FaceCapturedVideo)
                    == SearchScope.FaceCapturedVideo)
                {
                    if (v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((_scope & SearchScope.MotionWithoutFaceVideo)
                    == SearchScope.MotionWithoutFaceVideo)
                {
                    if (v.HasMotionDetected && !v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((_scope & SearchScope.MotionLessVideo)
                    == SearchScope.MotionLessVideo)
                {
                    if (!v.HasFaceCaptured &&
                        !v.HasMotionDetected)
                    {
                        _screen.AddVideo(v);
                    }
                }

            }

            _screen.Busy = false;
        }
    }
}
