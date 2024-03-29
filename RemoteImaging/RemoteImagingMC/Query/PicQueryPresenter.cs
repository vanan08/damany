﻿using System;
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
            this.view.NextPageClick += new EventHandler(view_NextPageClick);
            this.view.PreviousPageClick += new EventHandler(view_PreviousPageClick);
            this.view.FirstPageClick += new EventHandler(view_FirstPageClick);
            this.view.LastPageClick += new EventHandler(view_LastPageClick);
            this.view.PageSizeChanged += new EventHandler(view_PageSizeChanged);

            this.view.DownLoadVideoFileClick += new EventHandler(view_DownLoadVideoFileClick);
        }

        void view_DownLoadVideoFileClick(object sender, EventArgs e)
        {
            var ip = this.view.SelectedIP;
            var time = this.view.SelectedTime;
            var progress = this.view.ProgressIndicator;

            System.Threading.ThreadPool.QueueUserWorkItem( o => 
                this.DownloadVideoFileAt(ip, 2, time, this.view.DestinationStream, progress)
                );

        }

        void view_PageSizeChanged(object sender, EventArgs e)
        {
            this.CalcPagesCount();
        }

        private bool IsPagesDownloaded()
        {
            return this.totalPages != 0;
        }


        void view_LastPageClick(object sender, EventArgs e)
        {
            if ( !IsPagesDownloaded() ) return;

            this.CurrentPage = this.totalPages - 1;
            this.ShowCurrentPageAsync();
        }

        void view_FirstPageClick(object sender, EventArgs e)
        {
            if ( !IsPagesDownloaded() ) return;

            this.CurrentPage = 0;
            this.ShowCurrentPageAsync();
        }


        void view_PreviousPageClick(object sender, EventArgs e)
        {
            if (CurrentPage <= 0) return;

            CurrentPage--;

            this.ShowCurrentPageAsync();
        }

        void view_NextPageClick(object sender, EventArgs e)
        {
            if (CurrentPage >= totalPages - 1) return;

            CurrentPage++;
            this.ShowCurrentPageAsync();

        }


        private void ClearViewAsync()
        {
            this.syncContext.Post(o => this.view.ClearCurPageList(), null);
        }


        private void DoShowCurrent()
        {
            ClearViewAsync();

            for (int i = (CurrentPage) * view.PageSize;
                (i < (CurrentPage + 1) * view.PageSize) && (i < imagesFound.Length);
                ++i)
            {
                ImagePair ip = null;

                try
                {
                    ip = Gateways.Search.Instance.GetFace(view.SelectedIP, imagesFound[i]);
                }
                catch (System.ServiceModel.CommunicationException)
                {
                    this.syncContext.Post(view.ShowErrorMessage, "通讯错误, 请重试");
                    break;
                }

                this.syncContext.Post(o => this.view.AddFace(o as ImagePair), ip);

            }
        }

        public void DownloadVideoFileAt(
                                        System.Net.IPAddress ip, 
                                        int cameraId, 
                                        DateTime time, 
                                        System.IO.Stream destStream, 
                                        Damany.RemoteImaging.Common.IProgress progress
                                        )
        {
            var path = Gateways.Search.Instance.VideoFilePathRecordedAt(ip, time, cameraId);
            if (string.IsNullOrEmpty(path))
            {
                throw new System.IO.FileNotFoundException();
            }

            long size = Gateways.Search.Instance.GetFileSizeInBytes(ip, cameraId, path);
            
            var buffer = new byte[64 * 1024];
            using (progress)
            using (System.IO.Stream fs = Gateways.Search.Instance.DownloadFile(ip, path))
            using (destStream)
            {
                long bytesCount = 0;
                while (true)
                {
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    destStream.Write(buffer, 0, bytesRead);
                    bytesCount += bytesRead;

                    double percent = (double) bytesCount / size;
                    percent *= 100;

                    progress.Percent = (int)  percent;
                    System.Diagnostics.Debug.WriteLine(percent);

                }


            }

        }

        void ShowCurrentPageAsync()
        {
            this.syncContext = System.Threading.SynchronizationContext.Current;

            System.Threading.ThreadPool.QueueUserWorkItem(o => this.DoShowCurrent() );
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

            ShowCurrentPageAsync();
            
        }

        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                this.view.CurrentPage = value+1;
            }
        }


        int totalPages;
        private int CalcPagesCount()
        {
            if (this.imagesFound == null)
            {
                return 0;
            }

            totalPages = (imagesFound.Length + view.PageSize - 1) / this.view.PageSize;
            this.view.TotalPage = totalPages;

            return totalPages;
        }

    }
}
