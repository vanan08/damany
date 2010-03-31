using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class PicQueryFormPresenter : IPicQueryPresenter
    {
        public PicQueryFormPresenter( IPicQueryScreen screen, 
            Damany.PortraitCapturer.DAL.IRepository repository )
        {
            this.screen = screen;
            this.repository = repository;
        }

        #region IPicQueryPresenter Members

        public void Search()
        {
            this.range = this.screen.TimeRange;

            Action disableUI = delegate { EnableScreen(false); this.screen.ShowStatus("开始搜索");};
            Action enableUI = delegate { EnableScreen(true); this.screen.ShowStatus("搜索完毕"); };

            this.DoActionsAsync(disableUI, enableUI, 
                (Action)SearchInternal, 
                (Action)UpdateScreenPagesLabel, 
                (Action)ShowCurrentPage);
            
        }

        public void SelectedItemChanged()
        {
            if (this.screen.SelectedItem == null) return;

            var item = this.screen.SelectedItem;

            this.screen.CurrentPortrait = item;
            this.screen.CurrentBigPicture = this.repository.GetFrame(item.FrameId).GetImage().ToBitmap();
            
        }

        public void Start()
        {
            this.screen.AttachPresenter(this);
            this.screen.Show();
        }


        private void ShowCurrentPage()
        {
            if (this.portraits == null) return;

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

            if (this.currentPageIndex < this.totalPagesCount-1)
            {
                this.currentPageIndex++;
                UpdateCurrentPageAsync();
            }
        }

        public void NavigateToLast()
        {
            if (this.portraits == null) return;

            this.currentPageIndex = this.totalPagesCount - 1;
            UpdateCurrentPageAsync();
            
        }

        public void NavigateToFirst()
        {
            if (this.portraits == null) return;

            this.currentPageIndex = 0;
            UpdateCurrentPageAsync();
        }


        public void PageSizeChanged()
        {
            if (this.portraits == null) return;

            Action pre = delegate { this.screen.Clear(); this.EnableScreen(false); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after,  this.ShowCurrentPage);
        }

        #endregion
       
        private void DoActionsAsync(Action entryAction, Action exitAction, params Action[] actions)
        {
            if (entryAction !=null)
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
                        System.Threading.Thread.Sleep(1000);
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

        private void UpdateScreenPagesLabel()
        {
            if (this.portraits.Count > 0)
            {
                this.screen.Clear();
                this.screen.CurrentPage = this.currentPageIndex + 1;
               
                this.totalPagesCount = (this.portraits.Count + this.screen.PageSize - 1) / this.screen.PageSize;
                this.screen.TotalPage = this.totalPagesCount;
            }
        }
        private void SearchInternal()
        {
            this.portraits = this.repository.GetPortraits(this.range);
        }

        private void EnableScreen(bool enable)
        {
            this.screen.ShowUserIsBusy(!enable);
            this.screen.EnableSearchButton(enable);
            this.screen.EnableNavigateButtons(enable);
        }

        private void UpdateCurrentPageAsync()
        {
            Action pre = delegate { this.EnableScreen(false); this.UpdateScreenPagesLabel(); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after, this.ShowCurrentPage);
        }

        private Damany.Util.DateTimeRange range;
        IPicQueryScreen screen;
        Damany.PortraitCapturer.DAL.IRepository repository;
        IList<Damany.Imaging.Common.Portrait> portraits;
        int currentPageIndex;
        int totalPagesCount;

    }
}
