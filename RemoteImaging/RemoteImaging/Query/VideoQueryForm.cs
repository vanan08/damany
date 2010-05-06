using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Damany.PC.Domain;
using Damany.Util;
using RemoteImaging.Core;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Damany.RemoteImaging.Common;

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : Form, IVideoQueryScreen
    {
        public VideoQueryForm()
        {
            InitializeComponent();

            PopulateSearchScope();
            setListViewColumns();

            var now = DateTime.Now;
            this.timeTO.EditValue = now;
            this.timeFrom.EditValue = now.AddDays(-1);
        }


        public void SetCameras(IList<Damany.PC.Domain.CameraInfo> cameras)
        {
            if (cameras == null)
                throw new ArgumentNullException("cameras", "cameras is null.");

            foreach (var c in cameras)
            {
                this.cameraComboBox.Items.Add(c.Id.ToString());
            }

        }

        private void PopulateSearchScope()
        {
            var searchTypes = new List<SearchCategory>();
            searchTypes.Add( new SearchCategory{ Name = "全部",  Scope= SearchScope.All }  );
            searchTypes.Add(new SearchCategory { Name = "有人像视频", Scope = SearchScope.FaceCapturedVideo });
            searchTypes.Add( new SearchCategory{ Name = "有动态无人像视频",  Scope= SearchScope.MotionWithoutFaceVideo } );
            searchTypes.Add( new SearchCategory{ Name = "无动态视频",  Scope= SearchScope.MotionLessVideo } );

            this.searchType.DataSource = searchTypes;
            this.searchType.DisplayMember = "Name";
            this.searchType.ValueMember = "Scope";
        }


        private void queryBtn_Click(object sender, EventArgs e)
        {
            _presenter.Search();
        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("抓拍时间", 150);
            videoList.Columns.Add("视频文件", 150);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.faceList.Clear();
            this.faceImageList.Images.Clear();
            this.Close();
        }

        private void videoList_ItemActivate(object sender, EventArgs e)
        {
            _presenter.PlayVideo();
            _presenter.ShowRelatedFaces();
        }

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }

        }

        private void picList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("图片路径："+this.picList.FocusedItem.Tag.ToString());
            ShowDetailPic(ImageDetail.FromPath(this.faceList.FocusedItem.Tag.ToString()));
        }

        private void ShowDetailPic(ImageDetail img)
        {
        }

        internal class SearchCategory
        {
            public string Name { get; set; }
            public Query.SearchScope Scope { get; set; }
        }



        #region IVideoQueryScreen Members

        public Damany.Util.DateTimeRange TimeRange
        {
            get {  return new DateTimeRange((DateTime) this.timeFrom.EditValue, (DateTime) this.timeTO.EditValue);  }
        }

        public SearchScope SearchScope
        {
            get {  return (SearchScope) this.searchType.SelectedValue; }
        }

        public CameraInfo SelectedCamera
        {
            get
            {
                return (CameraInfo) this.cameraComboBox.SelectedValue;
            }
        }

        public CameraInfo[] Cameras
        {
            set
            {
                this.cameraComboBox.DataSource = value;
                this.cameraComboBox.DisplayMember = "Name";
            }
        }

        public void ClearAll()
        {
            this.videoList.Items.Clear();
            this.ClearFacesList();
        }

        public void ClearFacesList()
        {
            this.faceImageList.Images.Clear();
            this.faceList.Clear();
        }

        public void AddFace(Damany.Imaging.Common.Portrait p)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action = this.AddFace;
                this.BeginInvoke(action, p);
                return;
            }

            this.faceImageList.Images.Add(p.GetIpl().ToBitmap());

            var lvi = new ListViewItem
            {
                Tag = p,
                Text = p.CapturedAt.ToString(),
                ImageIndex = this.faceImageList.Images.Count - 1
            };

            this.faceList.Items.Add(lvi);

        }

        public void AddVideo(RemoteImaging.Core.Video v)
        {
            string videoPath = v.Path;
            DateTime dTime = ImageSearch.getDateTimeStr(v.Path);//"2009-6-29 14:00:00"
            var item = new ListViewItem();
            item.Text = dTime.ToString();
            item.SubItems.Add(videoPath);
            item.Tag = v;

            if (v.HasFaceCaptured)
            {
                item.ImageIndex = 0;
            }
            else if (v.HasMotionDetected)
            {
                item.ImageIndex = 1;
            }
            else if (!v.HasFaceCaptured && !v.HasMotionDetected)
            {
                item.ImageIndex = 2;
            }


            this.videoList.Items.Add(item);

        }

        public void AttachPresenter(IVideoQueryPresenter presenter)
        {
            this._presenter = presenter;
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public new void Show()
        {
            ShowDialog(Application.OpenForms[0]);
        }

        #endregion

        private IVideoQueryPresenter _presenter;

        #region IVideoQueryScreen Members


        public void PlayVideoInPlace(string videoPath)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
            }
            this.axVLCPlugin21.playlist.items.clear();


            int idx = this.axVLCPlugin21.playlist.add(videoPath, null, null);
            this.axVLCPlugin21.playlist.playItem(idx);
        }

        #endregion

        #region IVideoQueryScreen Members


        public Video SelectedVideoFile
        {
            get
            {
                if (this.videoList.SelectedItems.Count == 0)
                {
                    return null;
                }

                return (Video) this.videoList.SelectedItems[0].Tag;
            }
        }

        #endregion
    }
}
