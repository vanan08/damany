﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RemoteImaging.Core;
using RemoteControlService;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {

        private HostsPool hosts;

        public PicQueryForm()
        {
            InitializeComponent();

            this.bestPicListView.Scrollable = true;
            this.bestPicListView.MultiSelect = false;
            this.bestPicListView.View = View.LargeIcon;
            this.bestPicListView.LargeImageList = facesList;

            this.pageSizeComboBox.SelectedIndex = 0;

            DateTime now = DateTime.Now;

            this.searchToTime.EditValue = now;
            this.searchFromTime.EditValue = now.AddDays(-1);


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

        public event EventHandler PlayVideoClick
        {
            add
            {
                this.toolStripButtonPlayVideo.Click += value;
            }
            remove
            {
                this.toolStripButtonPlayVideo.Click -= value;
            }
        }

        public event EventHandler<SaveEventArgs> SaveImageClick;

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

        public DateTime SearchFrom
        {
            get
            {
                return (DateTime) this.searchFromTime.EditValue;
            }
        }

        public DateTime SearchTo
        {
            get
            {
                return (DateTime) this.searchToTime.EditValue;
            }
        }

        public int PageSize
        {
            get
            {
                return int.Parse(this.pageSizeComboBox.SelectedItem as string);
            }
        }

        int currentPage;
        public int CurrentPage
        {
            set
            {
                this.currentPage = value;
                this.UpdatePagesLabel(value, this.totalPage);
            }
        }


        int totalPage;
        public int TotalPage
        {
            set
            {
                this.totalPage = value;
                this.UpdatePagesLabel(this.currentPage, value);
            }
        }

        public Image Face
        {
            set
            {
                var oldImage = this.facePictureBox.Image;
                this.facePictureBox.Image = value;

                if (oldImage != null)
                {
                    oldImage.Dispose();
                }
            }
        }


        public Image WholeImage
        {
            set
            {
                var oldImge = this.wholeImage.Image;

                this.wholeImage.Image = value;

                if (oldImge != null)
                {
                    oldImge.Dispose();
                }
            }
        }


        public void AddFace( ImagePair pair )
        {
            this.facesList.Images.Add(pair.Face);
            string text = System.IO.Path.GetFileName(pair.FacePath as string);
            ListViewItem item = new ListViewItem()
            {
                Tag = pair,
                Text = text,
                ImageIndex = this.facesList.Images.Count - 1,
            };
            this.bestPicListView.Items.Add(item);
        }


        private void UpdatePagesLabel(int currentPage, int totalPage)
        {
            this.toolStripLabelCurPage.Text = string.Format("第{0}/{1}页", currentPage, totalPage);
        }

       


        public void ClearCurPageList()
        {
            this.bestPicListView.Clear();
            this.facesList.Images.Clear();
        }

        public void ClearLists()
        {
            ClearCurPageList();
            this.imageList2.Images.Clear();
            this.facePictureBox.Image = null;
            this.wholeImage.Image = null;
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

        private void queryBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }


            Cursor.Current = Cursors.Default;


        }
        //以前
        //02_090702150918-0001.jpg -->大图片
        //02_090702152518-0006-0000.jpg--> 小图片

        //现在 
        //02_090807144104343.jpg-->大图片
        //02_090807144104343-0000.jpg-->小图片
        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            ImagePair ip = this.bestPicListView.FocusedItem.Tag as ImagePair;

            this.facePictureBox.Image = ip.Face;

            //detail infomation
            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string captureLoc = string.Format("抓拍地点: {0}", imgInfo.FromCamera);
            this.labelCaptureLoc.Text = captureLoc;

            string captureTime = string.Format("抓拍时间: {0}", imgInfo.CaptureTime);
            this.labelCaptureTime.Text = captureTime;


            this.wholeImage.Image = ip.BigImage;

        }


        private void PopulateBigPicList(string iconFile)
        {
            this.imageList2.Images.Clear();

            string[] files = ImageSearch.SelectedBestImageChanged(iconFile);
            if (files == null)
            {
                MessageBox.Show("没有搜索到对应的二级图片", "警告");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                this.imageList2.Images.Add(Image.FromFile(files[i]));
                string text = System.IO.Path.GetFileName(files[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = files[i],
                    Text = text,
                    ImageIndex = i
                };
            }
        }

        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {


        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.bestPicListView.Clear();
            this.facesList.Images.Clear();
            this.imageList2.Images.Clear();


            this.Close();
        }

        private void secPicListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void PicQueryForm_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripButtonFirstPage_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void ReceiveVideoStream()
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(20);
            }

            this.axVLCPlugin21.playlist.items.clear();

            this.axVLCPlugin21.playlist.items.clear();

            string mrl = string.Format("udp://@{0}", "239.255.12.12");

            int idx = this.axVLCPlugin21.playlist.add(mrl, null, "-vvv");

            this.axVLCPlugin21.playlist.playItem(idx);
        }


        public void ShowErrorMessage(object state)
        {
            this.ShowMessage(state as string, MessageBoxIcon.Error);
        }

        public void ShowInfoMessage(object state)
        {
            this.ShowMessage(state as string, MessageBoxIcon.Information);
        }


        public void ShowMessage(string msg, MessageBoxIcon icon)
        {
            MessageBox.Show(this, msg, this.Text,
                MessageBoxButtons.OK, icon);
        }


        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            if (this.bestPicListView.SelectedItems.Count != 1) return;

            ImagePair ip = this.bestPicListView.SelectedItems[0].Tag as ImagePair;

            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string video = null;

            try
            {
                video  = Gateways.Search.Instance.VideoFilePathRecordedAt( SelectedIP, imgInfo.CaptureTime, imgInfo.FromCamera );
            }
            catch (System.ServiceModel.CommunicationException)
            {
                ShowErrorMessage("通讯错误，请重试！");
                return;
            }

            if (string.IsNullOrEmpty(video))
            {
                ShowInfoMessage("未找到相关视频");
                return;
            }

            this.ReceiveVideoStream();

            try
            {
                Gateways.StreamPlayer.Instance.StreamVideo( SelectedIP, video);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowErrorMessage("未找到相关视频");
            	
            }

        }

        private void SaveSelectedImage()
        {
            if ((this.bestPicListView.Items.Count <= 0) || (this.bestPicListView.FocusedItem == null)) return;
            ImagePair ip = this.bestPicListView.FocusedItem.Tag as ImagePair;


            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);
            string bigImgPath = ip.BigImagePath;

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.RestoreDirectory = true;
                saveDialog.Filter = "Jpeg 文件|*.jpg";
                //saveDialog.FileName = filePath.Substring(filePath.Length - 27, 27);
                string fileName = Path.GetFileName(ip.FacePath);
                saveDialog.FileName = fileName;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (facePictureBox.Image != null)
                    {
                        string path = saveDialog.FileName;
                        facePictureBox.Image.Save(path);
                        path = path.Replace(fileName, Path.GetFileName(bigImgPath));
                        wholeImage.Image.Save(path);
                    }
                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveSelectedImage();
        }

        void EnsureClosePlayer()
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }
        }


        private void PicQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
