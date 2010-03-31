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
            var range = this.screen.TimeRange;

            this.screen.ShowUserIsBusy(true);
            this.screen.EnableSearchButton(false);
            this.screen.EnableNavigateButtons(false);

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {

                try
                {
                    this.portraits = this.repository.GetPortraits(range);

                    if (this.portraits.Count > 0) this.screen.Clear();

                    ShowCurrentPage();
                }
                finally
                {
                    this.screen.EnableSearchButton(true);
                    this.screen.ShowUserIsBusy(false);
                    this.screen.EnableNavigateButtons(true);
                }
            });
        }

        public void SelectedItemChanged()
        {
            
        }

        public void Start()
        {
            this.screen.AttachPresenter(this);
            this.screen.Show();
        }


        #endregion

        private void ShowCurrentPage()
        {
            var page = this.portraits.Skip(this.currentPageIndex * this.screen.PageSize).Take(this.screen.PageSize);

            foreach (var item in page)
            {
                this.screen.AddItem(item);
            }
        }

        IPicQueryScreen screen;
        Damany.PortraitCapturer.DAL.IRepository repository;
        IList<Damany.Imaging.Common.Portrait> portraits;
        int currentPageIndex;

      
    }
}
