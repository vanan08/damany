using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;
using RemoteImaging.Gateways;
using System.Windows.Forms;

namespace RemoteImaging.Query
{
    class VideoQueryPresenter
    {
        VideoQueryForm view;

        public VideoQueryPresenter(VideoQueryForm view)
        {
            this.view = view;

            this.view.QueryClick += new EventHandler(view_QueryClick);
            this.view.SelectVideoFile += new EventHandler(view_SelectVideoFile);
            
        }

        void view_SelectVideoFile(object sender, EventArgs e)
        {
            this.view.ClearFacesList();

            DateTime time = ImageSearch.getDateTimeStr( this.view.SelectedVideoFile );
            this.SearchFacesCapturedAt(this.view.SelectedIP, 2, time);
        }

        void view_QueryClick(object sender, EventArgs e)
        {
            this.SearchVideos( this.view.SelectedIP, 2, this.view.SearchFrom, this.view.SearchTo);
        }

        private void SearchVideos(System.Net.IPAddress ip, int cameraID, DateTime from, DateTime to)
        {

            Video[] videos = null;

            try
            {
                videos = Search.Instance.SearchVideos(ip, 2, from, to);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("通讯错误, 请重试");
                return;
            }

            if (videos.Length == 0)
            {
                MessageBox.Show("没有搜索到满足条件的视频！", "警告");
                return;
            }

            this.view.ClearVideoFileList();
            this.view.VideoFiles = videos;
        }

        public void SearchFacesCapturedAt(System.Net.IPAddress ip, int cameraID, DateTime time)
        {
            ImagePair[] faces = null;
            try
            {
                faces = Gateways.Search.Instance.FacesCapturedAt(ip, 2, time);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("通讯错误, 请重试");

                return;
            }


            if (faces.Length == 0) return;

            foreach (var aFace in faces)
            {
                this.view.AddFace(aFace);
            }

        }
    }
}
