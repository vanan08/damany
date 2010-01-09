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

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {
        private string[] imagesFound = new string[0];
        private int currentPage;
        private int totalPage;
        public int PageSize { get; set; }

        int lastSpotID = 0;
        DateTime lastBeginTime = DateTime.Now;
        DateTime lastEndTime = DateTime.Now;
        RemoteControlService.ISearch SearchProxy;
        RemoteControlService.IStreamPlayer StreamProxy;

        private HostsPool hosts;


        public PicQueryForm()
        {
            InitializeComponent();

            this.PageSize = 20;
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
        private void UpdatePagesLabel()
        {
            this.toolStripLabelCurPage.Text = string.Format("第{0}/{1}页", currentPage, totalPage);
        }

        private int CalcPagesCount()
        {

            totalPage = (imagesFound.Length + PageSize - 1) / PageSize;

            return totalPage;
        }

        void ShowCurrentPage()
        {
            bestPicListView.BeginUpdate();

            ClearCurPageList();

            for (int i = (currentPage - 1) * PageSize;
                (i < currentPage * PageSize) && (i < imagesFound.Length);
                ++i)
            {
                ImagePair ip = null;

                try
                {
                    ip = SearchProxy.GetFace(imagesFound[i]);
                }
                catch (System.ServiceModel.CommunicationException)
                {
                    MessageBox.Show("通讯错误, 请重试");
                    IChannel ch = SearchProxy as IChannel;
                    if (ch.State == CommunicationState.Faulted)
                    {
                        this.CreateProxy();
                    }
                    break;
                }
                

                this.imageList1.Images.Add(ip.Face);
                string text = System.IO.Path.GetFileName(ip.FacePath as string);
                ListViewItem item = new ListViewItem()
                {
                    Tag = ip,
                    Text = text,
                    ImageIndex = i % PageSize
                };
                this.bestPicListView.Items.Add(item);
            }

            bestPicListView.EndUpdate();

        }

        private void ClearCurPageList()
        {
            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();
        }

        private void ClearLists()
        {
            ClearCurPageList();
            this.imageList2.Images.Clear();
            this.pictureBoxFace.Image = null;
            this.pictureBoxWholeImg.Image = null;
        }

        private DateTime CreateDateTime(
            DateTimePicker picker,
            DevExpress.XtraEditors.TimeEdit time)
        {
            DateTime date = picker.Value;
            DateTime t = time.Time;

            DateTime dt = new DateTime(date.Year, date.Month, date.Day, t.Hour, t.Minute, t.Second);

            return dt;
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
            Host selected = this.comboBox1.SelectedItem as Host;

            if (selected == null)
            {
                throw new Exception("No camera selected");
            }

            string searchAddress = string.Format("net.tcp://{0}:8000/TcpService", GetSelectedIP());
            string playerAddress = string.Format("net.tcp://{0}:4567/TcpService", GetSelectedIP());

            this.SearchProxy = ServiceProxy.ProxyFactory.CreateProxy<ISearch>(searchAddress);
            this.StreamProxy = ServiceProxy.ProxyFactory.CreateProxy<IStreamPlayer>(playerAddress);
        }


        private void queryBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }

            Camera selectedCamera = this.comboBox1.SelectedItem as Camera;

            //judge the input validation
            DateTime dateTime1 = CreateDateTime(this.dateTimePicker1, timeEdit1);
            DateTime dateTime2 = CreateDateTime(this.dateTimePicker2, timeEdit2);

            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！", "警告");
                return;
            }

            if (StreamProxy != null && isPlaying)
            {
                try
                {
                    StreamProxy.Stop();
                    isPlaying = false;
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {
                    
                }
                
            }

            CreateProxy();
            try
            {
                imagesFound = SearchProxy.SearchFaces(2, dateTime1, dateTime2);
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

            

            if (imagesFound.Length == 0)
            {
                MessageBox.Show(this, "未找到图片");
                return;
            }


            CalcPagesCount();
            currentPage = 1;
            UpdatePagesLabel();


            if (imagesFound == null)
            {
                MessageBox.Show("没有搜索到满足条件的图片！", "警告");
                return;
            }

            this.bestPicListView.Scrollable = true;
            this.bestPicListView.MultiSelect = false;
            this.bestPicListView.View = View.LargeIcon;
            this.bestPicListView.LargeImageList = imageList1;


            ShowCurrentPage();

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

            this.pictureBoxFace.Image = ip.Face;

            //detail infomation
            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string captureLoc = string.Format("抓拍地点: {0}", imgInfo.FromCamera);
            this.labelCaptureLoc.Text = captureLoc;

            string captureTime = string.Format("抓拍时间: {0}", imgInfo.CaptureTime);
            this.labelCaptureTime.Text = captureTime;


            this.pictureBoxWholeImg.Image = ip.BigImage;

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
            this.imageList1.Images.Clear();
            this.imageList2.Images.Clear();


            this.Close();
        }

        private void secPicListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void PicQueryForm_Load(object sender, EventArgs e)
        {
            this.toolStripComboBoxPageSize.Text = this.PageSize.ToString();
        }

        private void toolStripButtonFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {
            --currentPage;

            if (currentPage <= 0)
            {
                currentPage = 1;
                return;
            }

            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            ++currentPage;

            if (currentPage > totalPage)
            {
                currentPage = totalPage;
                return;
            }

            ShowCurrentPage();
            UpdatePagesLabel();
        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPage;

            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pageSize = (string)this.toolStripComboBoxPageSize.SelectedItem;

            // if (string.IsNullOrEmpty(pageSize)) return;

            int sz = int.Parse(pageSize);

            this.PageSize = sz;

            CalcPagesCount();
            currentPage = 1;
            UpdatePagesLabel();

            ShowCurrentPage();

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

        bool isPlaying = false;

        private void ShowErrorMessage()
        {
            MessageBox.Show(this, "通讯错误, 请重试！", this.Text, 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            if (SearchProxy == null) return;
            if (this.bestPicListView.SelectedItems.Count != 1) return;

            ImagePair ip = this.bestPicListView.SelectedItems[0].Tag as ImagePair;

            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string video = null;

            try
            {
                video  = SearchProxy.VideoFilePathRecordedAt(imgInfo.CaptureTime, imgInfo.FromCamera);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                ShowErrorMessage();
                IChannel ch = SearchProxy as IChannel;
                if (ch.State == CommunicationState.Faulted)
                {
                    this.CreateProxy();
                }
                return;
            }

            

            if (string.IsNullOrEmpty(video))
            {
                MessageBox.Show("未找到相关视频");
                return;
            }

            this.ReceiveVideoStream();

            try
            {
                StreamProxy.StreamVideo(video);
                isPlaying = true;
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowErrorMessage();
            	
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
                    if (pictureBoxFace.Image != null)
                    {
                        string path = saveDialog.FileName;
                        pictureBoxFace.Image.Save(path);
                        path = path.Replace(fileName, Path.GetFileName(bigImgPath));
                        pictureBoxWholeImg.Image.Save(path);
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

            if (StreamProxy != null && isPlaying)
            {
                StreamProxy.Stop();
            }

        }


        private void PicQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnsureClosePlayer();
        }
    }
}
