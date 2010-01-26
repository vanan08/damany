using System;
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
using System.Reflection;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {

        private HostsPool hosts;

        public PicQueryForm()
        {
            InitializeComponent();

            this.facesListView.Scrollable = true;
            this.facesListView.MultiSelect = false;
            this.facesListView.View = View.LargeIcon;
            this.facesListView.LargeImageList = facesList;

            Damany.RemoteImaging.Common.ControlHelper.SetControlProperty(this.facesListView, "DoubleBuffered", true);


            this.pageSizeComboBox.SelectedIndex = 0;

            DateTime now = DateTime.Now;

            this.searchToTime.EditValue = now;
            this.searchFromTime.EditValue = now.AddDays(-1);

        }

        public event EventHandler PageSizeChanged;
        public event EventHandler DownLoadVideoFileClick;
        public event EventHandler QueryClick;
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

        public event EventHandler NextPageClick;

        public event EventHandler PreviousPageClick;

        public event EventHandler LastPageClick;

        public event EventHandler FirstPageClick;


        public Damany.RemoteImaging.Common.IProgress ProgressIndicator
        {
            get
            {
                Damany.RemoteImaging.Common.Forms.ProgressForm progress =
                    new Damany.RemoteImaging.Common.Forms.ProgressForm();

                if (this.InvokeRequired)
                {
                    Action<System.Windows.Forms.Form> show = progress.Show;

                    show.Invoke(this);
                }
                else
                {
                    progress.Show(this);

                }

                return progress;
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

                this.hostsComboBox.DataSource = value.Hosts;
                this.hostsComboBox.DisplayMember = "Name";
            }
        }

        public DateTime SearchFrom
        {
            get
            {
                return (DateTime)this.searchFromTime.EditValue;
            }
        }

        public DateTime SearchTo
        {
            get
            {
                return (DateTime)this.searchToTime.EditValue;
            }
        }

        private int GetPageSize()
        {
            return int.Parse(this.pageSizeComboBox.SelectedItem as string);
        }

        public int PageSize
        {
            get
            {
                if (this.InvokeRequired)
                {
                    Func<int> getPgSz = () => GetPageSize();

                    return (int)this.Invoke(getPgSz);
                }
                else
                    return GetPageSize();
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

        public DateTime SelectedTime
        {
            get
            {
                if (this.facesListView.FocusedItem == null) return default(DateTime);

                ImagePair ip = this.facesListView.FocusedItem.Tag as ImagePair;

                ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

                return imgInfo.CaptureTime;

            }

        }


        public void AddFace(ImagePair pair)
        {
            this.facesList.Images.Add(pair.Face);
            string text = System.IO.Path.GetFileName(pair.FacePath as string);
            ListViewItem item = new ListViewItem()
            {
                Tag = pair,
                Text = text,
                ImageIndex = this.facesList.Images.Count - 1,
            };
            this.facesListView.Items.Add(item);
        }


        private void UpdatePagesLabel(int currentPage, int totalPage)
        {
            this.toolStripLabelCurPage.Text = string.Format("第{0}/{1}页", currentPage, totalPage);
        }


        public void ClearCurPageList()
        {
            this.facesListView.Clear();

            foreach (Image image in this.facesList.Images)
            {
                image.Dispose();
            }

            this.facesList.Images.Clear();
        }

        public void ClearLists()
        {
            ClearCurPageList();
            this.imageList2.Images.Clear();
            this.facePictureBox.Image = null;
            this.wholeImage.Image = null;
        }

        private System.Net.IPAddress InternalGetSelectedIP()
        {
            Host selected = this.hostsComboBox.SelectedItem as Host;
            if (selected == null)
            {
                throw new Exception("No camera selected");
            }

            return selected.Ip;

        }

        public System.Net.IPAddress SelectedIP
        {
            get
            {
                if (this.InvokeRequired)
                {
                    Func<System.Net.IPAddress> getIp = () => InternalGetSelectedIP();

                    return (System.Net.IPAddress)this.Invoke(getIp);
                }
                else
                    return InternalGetSelectedIP();

            }

        }

        private bool IsHostSelected()
        {
            if (this.hostsComboBox.SelectedValue == null)
            {
                this.ShowErrorMessage("请选择要查询的监控点");
                return false;
            }

            return true;
        }
        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (!IsHostSelected()) return;

            if (this.QueryClick != null)
            {
                this.QueryClick(this, EventArgs.Empty);
            }
        }


        //以前
        //02_090702150918-0001.jpg -->大图片
        //02_090702152518-0006-0000.jpg--> 小图片

        //现在 
        //02_090807144104343.jpg-->大图片
        //02_090807144104343-0000.jpg-->小图片
        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            ImagePair ip = this.facesListView.FocusedItem.Tag as ImagePair;

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


        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.facesListView.Clear();
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
            if (!IsHostSelected()) return;

            if (FirstPageClick != null)
            {
                FirstPageClick(sender, e);
            }

        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {
            if (!IsHostSelected()) return;

            if (PreviousPageClick != null)
            {
                PreviousPageClick(sender, e);
            }


        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            if (!IsHostSelected()) return;

            if (NextPageClick != null)
            {
                NextPageClick(sender, e);
            }

        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {
            if (!IsHostSelected()) return;

            if (LastPageClick != null)
            {
                LastPageClick(sender, e);
            }


        }

        private void toolStripComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PageSizeChanged != null)
            {
                this.PageSizeChanged(this, EventArgs.Empty);
            }

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
            if (this.facesListView.SelectedItems.Count != 1) return;

            ImagePair ip = this.facesListView.SelectedItems[0].Tag as ImagePair;

            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string video = null;

            try
            {
                video = Gateways.Search.Instance.VideoFilePathRecordedAt(SelectedIP, imgInfo.CaptureTime, imgInfo.FromCamera);
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
                Gateways.StreamPlayer.Instance.StreamVideo(SelectedIP, video);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowErrorMessage("未找到相关视频");

            }

        }

        private void SaveSelectedImage()
        {
            if ((this.facesListView.Items.Count <= 0) || (this.facesListView.FocusedItem == null)) return;
            ImagePair ip = this.facesListView.FocusedItem.Tag as ImagePair;


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


        public System.IO.Stream DestinationStream { get { return saveVideoFileDialog.OpenFile(); } }

        private void downloadVideoFile_Click(object sender, EventArgs e)
        {
            if (this.SelectedTime == default(DateTime))
            {
                ShowErrorMessage("请选择一幅图片！");
                return;
            }

            if (saveVideoFileDialog.ShowDialog(this) == DialogResult.Cancel) return;

            if (this.DownLoadVideoFileClick != null)
            {
                this.DownLoadVideoFileClick(this, EventArgs.Empty);
            }

        }

        private void PicQueryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
