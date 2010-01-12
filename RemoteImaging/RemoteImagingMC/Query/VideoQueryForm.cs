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
        }

        private System.Net.IPAddress GetSelectedIP()
        {
            Host selected = this.comboBox1.SelectedItem as Host;

            if (selected == null)
            {
                throw new Exception("No camera selected");
            }

            return selected.Ip;
        }

        private void CreateProxy()
        {
            StreamServerProxy = ServiceProxy.ProxyFactory.CreatePlayerProxy(GetSelectedIP().ToString());
            SearchProxy = ServiceProxy.ProxyFactory.CreateSearchProxy(GetSelectedIP().ToString());
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.picList.Clear();

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }

            Camera selectedCamera = this.comboBox1.SelectedItem as Camera;


            //judge the input validation
            DateTime date1 = this.dateTimePicker1.Value;
            DateTime date2 = this.dateTimePicker2.Value;
            DateTime time1 = this.timeEdit1.Time;
            DateTime time2 = this.timeEdit2.Time;

            DateTime dateTime1 = new DateTime(date1.Year, date1.Month, date1.Day, time1.Hour, time1.Minute, time1.Second);
            DateTime dateTime2 = new DateTime(date2.Year, date2.Month, date2.Day, time2.Hour, time2.Minute, time2.Second);
            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！", "警告");
                return;
            }

            if (StreamServerProxy != null && IsPlaying)
            {
                try
                {
                    StreamServerProxy.Stop();
                    IsPlaying = false;
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {

                }
            }

            CreateProxy();
            Video[] videos = null;



            try
            {
                videos = SearchProxy.SearchVideos(2, dateTime1, dateTime2);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("通讯错误, 请重试");
                IChannel ch = SearchProxy as IChannel;
                if (ch.State == CommunicationState.Faulted)
                {
                    this.CreateProxy();
                }

                return;
            }


            if (videos.Length == 0)
            {
                MessageBox.Show("没有搜索到满足条件的视频！", "警告");
                return;
            }

            this.videoList.Items.Clear();

            foreach (Video v in videos)
            {
                string videoPath = v.Path;
                DateTime dTime = ImageSearch.getDateTimeStr(videoPath);//"2009-6-29 14:00:00"
                ListViewItem lvl = new ListViewItem();
                lvl.Text = dTime.ToString();
                lvl.SubItems.Add(videoPath);
                lvl.Tag = videoPath;

                if (this.faceCapturedRadioButton.Checked)
                {
                    if (v.HasFaceCaptured)
                    {
                        lvl.ImageIndex = 0;
                        videoList.Items.Add(lvl);
                    }
                }

                if (this.allVideoRadioButton.Checked)
                {
                    if (v.HasFaceCaptured)
                        lvl.ImageIndex = 0;
                    else
                        lvl.ImageIndex = 1;
                    videoList.Items.Add(lvl);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("抓拍时间", 150);
            videoList.Columns.Add("视频文件", 150);
            faceCapturedRadioButton.Checked = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();
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

        bool IsPlaying = false;

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
            IsPlaying = true;


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
            this.picList.Clear();
            this.imageList1.Images.Clear();

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
                IChannel ch = SearchProxy as IChannel;
                if (ch.State == CommunicationState.Faulted)
                {
                    this.CreateProxy();
                }

                return;
            }


            if (faces.Length == 0) return;

            foreach (var aFace in faces)
            {
                this.imageList1.Images.Add(aFace.Face);
                string text = System.IO.Path.GetFileName(aFace.FacePath);
                ListViewItem item = new ListViewItem
                {
                    Tag = aFace,
                    Text = text,
                    ImageIndex = this.imageList1.Images.Count - 1,
                };
                this.picList.Items.Add(item);
            }

            this.picList.Scrollable = true;
            this.picList.MultiSelect = false;
            this.picList.View = View.LargeIcon;
            this.picList.LargeImageList = imageList1;
        }

        #endregion

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }

            if (StreamServerProxy != null && IsPlaying)
            {
                StreamServerProxy.Stop();
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
            this.CreateProxy();

            var stream = this.SearchProxy.DownloadFile(filePathToDownload.Text, string.Empty);

            byte[] buffer = new byte[1024];
            int totalBytes = 0;
            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }


                Debug.WriteLine(string.Format("read {0} bytes", bytesRead));
                totalBytes += bytesRead;
            }

            Debug.WriteLine(totalBytes);
        }

        private void downloadBmp_Click(object sender, EventArgs e)
        {
            var bmp = new System.Drawing.Bitmap(30, 30);
            System.Diagnostics.Debug.Assert(bmp is System.Xml.Serialization.IXmlSerializable);
            
            this.CreateProxy();

            bmp = this.SearchProxy.DownloadBitmap(filePathToDownload.Text);
        }
    }
}
