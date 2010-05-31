using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class PicQueryFormPresenter : IPicQueryPresenter
    {
        public PicQueryFormPresenter(IPicQueryScreen screen,
                                      IRepository repository,
                                      ConfigurationManager configManager)
        {
            this.screen = screen;
            this.repository = repository;
            _configManager = configManager;
        }

        #region IPicQueryPresenter Members

        public void Search()
        {
            this.range = this.screen.TimeRange;
            selectedCamera = screen.SelectedCamera;

            if (selectedCamera == null)
            {
                return;
            }

            Action disableUI = delegate
            {
                EnableScreen(false);
                this.screen.ShowStatus("开始搜索");
            };

            Action enableUI = delegate
            {
                EnableScreen(true);
                this.screen.ShowStatus("搜索完毕");
            };

            this.DoActionsAsync(disableUI, enableUI,
                (Action)SearchInternal,
                (Action)CalculatePaging,
                (Action)ShowCurrentPage);

        }

        public void PlayVideo()
        {
            var p = this.screen.SelectedItem;

            if (p != null) VideoPlayer.PlayRelatedVideo(p);
        }

        public void SelectedItemChanged()
        {
            if (this.screen.SelectedItem == null) return;

            var item = this.screen.SelectedItem;

            this.screen.CurrentPortrait = item;

            var frame = repository.GetFrame(item.FrameId);

            if (frame != null)
            {
                this.screen.CurrentBigPicture = frame.GetImage().ToBitmap();
            }


        }

        public void Start()
        {
            this.screen.AttachPresenter(this);
            this.screen.Cameras = _configManager.GetCameras().ToArray();
            this.screen.Show();
        }


        private void ShowCurrentPage()
        {
            if (this.portraits == null) return;

            this.screen.Clear();

            var page = this.portraits.Skip(this.currentPageIndex * this.screen.PageSize).Take(this.screen.PageSize);

            foreach (var item in page)
            {
                this.screen.AddItem(item);
            }
        }



        public void NavigateToPrev()
        {
            if (this.portraits == null) return;

            if (this.currentPageIndex > 0)
            {
                this.currentPageIndex--;

                UpdateCurrentPageAsync();
            }

        }

        public void NavigateToNext()
        {
            if (this.portraits == null) return;

            if (this.currentPageIndex < this.totalPagesCount - 1)
            {
                this.currentPageIndex++;
                UpdateCurrentPageAsync();
            }
        }

        public void NavigateToLast()
        {
            if (this.portraits == null) return;
            if (this.currentPageIndex == this.totalPagesCount - 1) return;

            this.currentPageIndex = this.totalPagesCount - 1;
            UpdateCurrentPageAsync();

        }

        public void NavigateToFirst()
        {
            if (this.portraits == null) return;
            if (this.currentPageIndex == 0) return;

            this.currentPageIndex = 0;
            UpdateCurrentPageAsync();
        }


        public void PageSizeChanged()
        {
            if (this.portraits == null) return;

            this.CalculatePaging();

            Action pre = delegate { this.screen.Clear(); this.EnableScreen(false); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after, this.ShowCurrentPage);
        }

        #endregion

        private void DoActionsAsync(Action entryAction, Action exitAction, params Action[] actions)
        {
            if (entryAction != null)
            {
                entryAction();
            }


            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    foreach (var item in actions)
                    {
                        item();
                    }
                }
                finally
                {
                    if (exitAction != null)
                    {
                        exitAction();
                    }

                }
            });
        }

        private void CalculatePaging()
        {
            if (this.portraits.Count > 0)
            {
                this.totalPagesCount = (this.portraits.Count + this.screen.PageSize - 1) / this.screen.PageSize;

                if (this.currentPageIndex > this.totalPagesCount - 1)
                {
                    this.currentPageIndex = this.totalPagesCount - 1;
                }

                this.screen.CurrentPage = this.currentPageIndex + 1;
                this.screen.TotalPage = this.totalPagesCount;
            }
        }

        private void SearchInternal()
        {
            this.portraits = this.repository.GetPortraits(selectedCamera.CameraId, this.range);
        }

        private void EnableScreen(bool enable)
        {
            this.screen.ShowUserIsBusy(!enable);
            this.screen.EnableSearchButton(enable);
            this.screen.EnableNavigateButtons(enable);
        }

        private void UpdateCurrentPageAsync()
        {
            Action pre = delegate { this.EnableScreen(false); this.CalculatePaging(); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after, this.ShowCurrentPage);
        }

        private Damany.Util.DateTimeRange range;
        IPicQueryScreen screen;
        IRepository repository;
        private readonly ConfigurationManager _configManager;
        IList<Damany.Imaging.Common.Portrait> portraits;
        int currentPageIndex;
        int totalPagesCount;
        Damany.PC.Domain.Destination selectedCamera;

    }
}
