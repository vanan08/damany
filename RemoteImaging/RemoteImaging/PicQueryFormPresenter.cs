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

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            this.screen.EnableSearchButton(false);
            

            try
            {
                this.repository.GetPortraits(range);
                System.Threading.Thread.Sleep(5000);
            }
            finally
            {
                this.screen.EnableSearchButton(true);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            }

            


            
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

        IPicQueryScreen screen;
        Damany.PortraitCapturer.DAL.IRepository repository;

      
    }
}
