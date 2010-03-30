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
