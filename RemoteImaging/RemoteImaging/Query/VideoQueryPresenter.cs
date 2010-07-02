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

            System.Threading.ThreadPool.QueueUserWorkItem(o =>
                                                              {

                                                                  _screen.Busy = true;

                                                                  var videos =
                                                                      new FileSystemStorage(
                                                                          Properties.Settings.Default.OutputPath).
                                                                          VideoFilesBetween(selectedCamera.Id,
                                                                                            range.From, range.To);

                                                                  if (videos.Count() < 0)
                                                                  {
                                                                      return;
                                                                  }

                                                                  var frameQuery = _portraitRepository.GetFramesQuery()
                                                                      .Where(
                                                                          frame =>
                                                                          frame.CapturedFrom.Id == selectedCamera.Id
                                                                          && frame.CapturedAt >= range.From &&
                                                                          frame.CapturedAt <= range.To);

                                                                  var frameHash = new HashSet<DateTime>();
                                                                  foreach (var g in frameQuery)
                                                                  {
                                                                      var round = g.CapturedAt.RoundToMinute();
                                                                      frameHash.Add(round);
                                                                  }


                                                                  var portraitQuery =
                                                                      _portraitRepository.GetPortraits(
                                                                          selectedCamera.Id, range).ToArray();
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


                                                                      if ((type & SearchScope.FaceCapturedVideo)
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
                                                                          if (!v.HasFaceCaptured &&
                                                                              !v.HasMotionDetected)
                                                                          {
                                                                              _screen.AddVideo(v);
                                                                          }
                                                                      }

                                                                  }

                                                                  _screen.Busy = false;
                                                              });
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
    }
}
