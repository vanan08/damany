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

            this.facesListView.Scrollable = true;
            this.facesListView.MultiSelect = false;
            this.facesListView.View = View.LargeIcon;
            this.facesListView.LargeImageList = faceImageList;


            Damany.RemoteImaging.Common.ControlHelper.SetControlProperty(this.facesListView, "DoubleBuffered", true);

            this.queryBtn.Click += new EventHandler(queryBtn_Click);

        }

        void queryBtn_Click(object sender, EventArgs e)
        {
            if (this.hostsComboBox.SelectedValue == null)
            {
                this.ShowErrorMessage("请选择要查询的监控点!");
                return;
            }

            if (this.QueryClick != null)
            {
                this.QueryClick(this, EventArgs.Empty);
            }
        }


        public event EventHandler QueryClick;
        public event EventHandler SelectVideoFile;

        public string SelectedVideoFile
        {
            get
            {
                if (this.videoList.SelectedItems.Count == 0) return null;

                return this.videoList.SelectedItems[0].Tag as string;
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
                Host selected = this.hostsComboBox.SelectedItem as Host;

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


        private void ShowErrorMessage(object state)
        {
            MessageBox.Show(this, state as string, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void videoList_ItemActivate(object sender, EventArgs e)
        {

            if (this.videoList.SelectedItems.Count == 0) return;

            if (this.SelectVideoFile != null)
            {
                this.SelectVideoFile(this, EventArgs.Empty);
            }

            if (string.IsNullOrEmpty(VideoPlayer.ExePath))
            {
                MessageBox.Show("请安装vlc播放器");
                return;
            }

            if (string.IsNullOrEmpty(this.SelectedVideoFile)) return;

            ReceiveVideoStream();

            Func<System.Net.IPAddress, string, bool> playVideo =
                Gateways.StreamPlayer.Instance.StreamVideo;

            playVideo.BeginInvoke(this.SelectedIP, this.SelectedVideoFile, null, null);



        }

        public void AddFace(ImagePair pair)
        {
            this.faceImageList.Images.Add(pair.Face);
            string text = System.IO.Path.GetFileName(pair.FacePath);
            ListViewItem item = new ListViewItem
            {
                Tag = pair,
                Text = text,
                ImageIndex = this.faceImageList.Images.Count - 1,
            };
            this.facesListView.Items.Add(item);

        }

        public void ClearFacesList()
        {
            foreach (Image img in this.faceImageList.Images)
            {
                img.Dispose();
            }

            this.faceImageList.Images.Clear();

            this.facesListView.Items.Clear();
        }



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

                this.hostsComboBox.DataSource = value.Hosts;
                this.hostsComboBox.DisplayMember = "Name";
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
