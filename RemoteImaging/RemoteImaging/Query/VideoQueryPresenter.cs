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

            var range = this._screen.TimeRange;
            var type = this._screen.SearchScope;


            Core.Video[] videos =
                FileSystemStorage.VideoFilesBetween(selectedCamera.Id, range.From, range.To);

            if (videos.Length == 0) return;

            var frameQuery = _portraitRepository.GetFrames(range).ToArray();
            var portraitQuery = _portraitRepository.GetPortraits(range).ToArray();

            this._screen.ClearAll();

            foreach (var v in videos)
            {
                var queryTime = new DateTimeRange(v.CapturedAt, v.CapturedAt);
                v.HasMotionDetected = frameQuery.FirstOrDefault(f => f.CapturedAt.RoundToMinute() == v.CapturedAt.RoundToMinute()) != null;
                v.HasFaceCaptured = portraitQuery.FirstOrDefault(p => p.CapturedAt.RoundToMinute() == v.CapturedAt.RoundToMinute()) != null;


                if (( type & SearchScope.FaceCapturedVideo)
                      == SearchScope.FaceCapturedVideo)
                {
                    if (v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((type & SearchScope.MotionWithoutFaceVideo)
                     == SearchScope.MotionWithoutFaceVideo)
                {
                    if (v.HasMotionDetected && !v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((type & SearchScope.MotionLessVideo)
                      == SearchScope.MotionLessVideo)
                {
                    if (!v.HasFaceCaptured && !v.HasMotionDetected)
                    {
                        _screen.AddVideo(v);
                    }
                }

            }
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

            var p = _portraitRepository.GetPortraits(range);
            _screen.ClearFacesList();

            foreach (var portrait in p)
            {
                 _screen.AddFace(portrait);
            }
           
        }
    }
}
