using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;

namespace RemoteImaging.Query
{
    public class PicQueryPresenter
    {
        PicQueryForm view;
        string[] imagesFound;
        int currentPage;
        System.Threading.SynchronizationContext syncContext;

        public PicQueryPresenter(PicQueryForm view)
        {
            this.view = view;
            this.syncContext = System.Threading.SynchronizationContext.Current;

            this.view.QueryClick += new EventHandler(view_QueryClick);
        }


        void ShowCurrentPage()
        {

            this.view.ClearCurPageList();

            for (int i = (currentPage) * view.PageSize;
                (i < (currentPage +1) * view.PageSize) && (i < imagesFound.Length);
                ++i)
            {
                ImagePair ip = null;

                try
                {
                    ip = Gateways.Search.Instance.GetFace(view.SelectedIP, imagesFound[i]);
                }
                catch (System.ServiceModel.CommunicationException)
                {
                    this.syncContext.Post( view.ShowErrorMessage,  "通讯错误, 请重试");
                    break;
                }

                this.view.AddFace(ip);
                
            }


        }

        void view_QueryClick(object sender, EventArgs e)
        {
            try
            {
                imagesFound = Gateways.Search.Instance.SearchFaces(view.SelectedIP, 2, view.SearchFrom, view.SearchTo);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.syncContext.Post( view.ShowErrorMessage, "通讯讯错误, 请重试");
                return;
            }



            if (imagesFound.Length == 0)
            {
                this.view.ShowInfoMessage("未找到图片");
                return;
            }


            CalcPagesCount();

            this.view.CurrentPage = 1;
            this.view.TotalPage = totalPages;


            if (imagesFound == null)
            {
                this.view.ShowInfoMessage("没有搜索到满足条件的图片！");
                return;
            }



            ShowCurrentPage();
            
        }

        int totalPages;
        private int CalcPagesCount()
        {

            totalPages = (imagesFound.Length + view.PageSize - 1) / this.view.PageSize;

            return totalPages;
        }





    }
}
