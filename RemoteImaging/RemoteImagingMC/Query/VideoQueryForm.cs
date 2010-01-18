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
using RemoteImaging.Core;
using RemoteControlService;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel;
using RemoteImaging.Gateways;

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : Form
    {
        private IStreamPlayer StreamServerProxy;
        private ISearch SearchProxy;

        private HostsPool hosts;

        public VideoQueryForm()
        {
            InitializeComponent();

            setListViewColumns();

            this.PopulateSearchScope();

            DateTime now = DateTime.Now;

            this.searchTo.EditValue = now;
            this.searchFrom.EditValue = now.AddDays(-1);

            Damany.RemoteImaging.Common.ControlHelper.SetControlProperty(this.facesListView, "DoubleBuffered", true);

        }


        public event EventHandler QueryClick
        {
            add
            {
                this.queryBtn.Click += value;
            }
            remove
            {
                this.queryBtn.Click -= value;
            }
        }


        private void PopulateSearchScope()
        {
            var searchTypes = new List<SearchCategory>();
            searchTypes.Add(new SearchCategory { Name = "全部", Scope = SearchScope.All });
            searchTypes.Add(new SearchCategory { Name = "有效视频", Scope = SearchScope.VideoWithFaces });
            searchTypes.Add(new SearchCategory { Name = "无效视频", Scope = SearchScope.VideoWithoutFaces });

            this.searchType.DataSource = searchTypes;
            this.searchType.DisplayMember = "Name";
            this.searchType.ValueMember = "Scope";
        }

        public SearchScope SelectedSearchScope
        {
            get
            {
                return (SearchScope)this.searchType.SelectedValue;
            }
        }


        public System.Net.IPAddress SelectedIP
        {
            get
            {
                Host selected = this.comboBox1.SelectedItem as Host;

                if (selected == null)
                {
                    throw new Exception("No camera selected");
                }

                return selected.Ip;

            }

        }

        public DateTime SearchFrom
        {
            get
            {
                return (DateTime)this.searchFrom.EditValue;
            }
        }

        public DateTime SearchTo
        {
            get
            {
                return (DateTime)this.searchTo.EditValue;
            }
        }

        public void ClearVideoFileList()
        {
            this.videoList.Items.Clear();
        }

        public Video[] VideoFiles
        {
            set
            {
                foreach (var v in value)
                {
                    string videoPath = v.Path;
                    DateTime dTime = ImageSearch.getDateTimeStr(videoPath);//"2009-6-29 14:00:00"
                    ListViewItem lvl = new ListViewItem();
                    lvl.Text = dTime.ToString();
                    lvl.SubItems.Add(videoPath);
                    lvl.Tag = videoPath;

                    if ((this.SelectedSearchScope & SearchScope.VideoWithFaces)
                           == SearchScope.VideoWithFaces)
                    {
                        if (v.HasFaceCaptured)
                        {
                            lvl.ImageIndex = 0;
                            videoList.Items.Add(lvl);
                        }
                    }

                    if ((this.SelectedSearchScope & SearchScope.VideoWithoutFaces)
                           == SearchScope.VideoWithoutFaces)
                    {
                        if (!v.HasFaceCaptured)
                        {
                            lvl.ImageIndex = 1;
                            videoList.Items.Add(lvl);
                        }
                    }

                }
            }
        }


        private void queryBtn_Click(object sender, EventArgs e)
        {

        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("抓拍时间", 150);
            videoList.Columns.Add("视频文件", 150);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.facesListView.Clear();
            this.faceImageList.Images.Clear();
            this.Close();
        }


        private void ReceiveVideoStream()
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(20);
            }

            this.axVLCPlugin21.playlist.items.clear();

            string mrl = string.Format("udp://@{0}", "239.255.12.12");

            int idx = this.axVLCPlugin21.playlist.add(mrl, null, "-vvv");

            this.axVLCPlugin21.playlist.playItem(idx);
        }


        private void ShowErrorMessage()
        {
            MessageBox.Show(this, "通讯错误, 请重试！", this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void videoList_ItemActivate(object sender, EventArgs e)
        {

            if (StreamServerProxy == null) return;
            if (this.videoList.SelectedItems.Count == 0) return;
            if (string.IsNullOrEmpty(VideoPlayer.ExePath))
            {
                MessageBox.Show("请安装vlc播放器");
                return;
            }

            ListViewItem item = this.videoList.SelectedItems[0];

            ReceiveVideoStream();

            try
            {
                StreamServerProxy.StreamVideo(item.Tag as string);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowErrorMessage();
            }




            bindPiclist();
        }


        #region 绑定picList()

        void bindPiclist()
        {
            this.facesListView.Clear();
            this.faceImageList.Images.Clear();

            DateTime time = ImageSearch.getDateTimeStr(videoList.FocusedItem.Tag as string);

            Camera selectedCamera = this.comboBox1.SelectedItem as Camera;

            ImagePair[] faces = null;

            try
            {
                faces = SearchProxy.FacesCapturedAt(2, time);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("通讯错误, 请重试");

                return;
            }


            if (faces.Length == 0) return;

            foreach (var aFace in faces)
            {
                this.faceImageList.Images.Add(aFace.Face);
                string text = System.IO.Path.GetFileName(aFace.FacePath);
                ListViewItem item = new ListViewItem
                {
                    Tag = aFace,
                    Text = text,
                    ImageIndex = this.faceImageList.Images.Count - 1,
                };
                this.facesListView.Items.Add(item);
            }

            this.facesListView.Scrollable = true;
            this.facesListView.MultiSelect = false;
            this.facesListView.View = View.LargeIcon;
            this.facesListView.LargeImageList = faceImageList;
        }

        #endregion

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }
        }


        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
        }

        private void VideoQueryForm_Load(object sender, EventArgs e)
        {
            ReceiveVideoStream();
        }


        public HostsPool Hosts
        {
            get
            {
                return hosts;
            }
            set
            {
                hosts = value;

                this.comboBox1.DataSource = value.Hosts;
                this.comboBox1.DisplayMember = "Name";
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {

        }

        private void downloadBmp_Click(object sender, EventArgs e)
        {
        }
    }

    [Flags]
    public enum SearchScope : byte
    {
        VideoWithFaces = 1,
        VideoWithoutFaces = 2,
        All = byte.MaxValue,
    }

    internal class SearchCategory
    {
        public string Name { get; set; }
        public SearchScope Scope { get; set; }
    }



}
