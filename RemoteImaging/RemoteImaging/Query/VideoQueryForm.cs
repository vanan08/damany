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

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : Form
    {
        public VideoQueryForm()
        {
            InitializeComponent();
            foreach (Camera camera in Configuration.Instance.Cameras)
            {
                this.comboBox1.Items.Add(camera.ID.ToString());
            }
            setListViewColumns();
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.axVLCPlugin21.Toolbar = true;

            this.picList.Clear();

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }


            int cameraID = int.Parse(this.comboBox1.Text);

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

            Video[] videos = FileSystemStorage.VideoFilesBetween(cameraID, dateTime1, dateTime2);

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

                if (faceCapturedVideoRadioButton.Checked == true)
                {
                    if (v.HasFaceCaptured)
                    {
                        lvl.ImageIndex = 0;
                        videoList.Items.Add(lvl);
                    }
                }

                if (AllVideoTypeRadioButton.Checked == true)
                {
                    if (v.HasFaceCaptured)
                        lvl.ImageIndex = 0;
                    else
                        lvl.ImageIndex = 1;
                    videoList.Items.Add(lvl);
                }


            }
        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("抓拍时间", 150);
            videoList.Columns.Add("视频文件", 150);
            faceCapturedVideoRadioButton.Checked = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();
            this.Close();
        }

        private void videoList_ItemActivate(object sender, EventArgs e)
        {
            bindPiclist();

            if (this.videoList.SelectedItems.Count == 0) return;

            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
            }

            this.axVLCPlugin21.playlist.items.clear();

            ListViewItem item = this.videoList.SelectedItems[0];

            int idx = this.axVLCPlugin21.playlist.add(item.Tag as string, null, null);

            this.axVLCPlugin21.playlist.playItem(idx);
        }


        #region 绑定picList()

        void bindPiclist()
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();

            DateTime time = ImageSearch.getDateTimeStr(videoList.FocusedItem.Tag as string);
            int cameID = int.Parse(this.comboBox1.Text);

            string[] fileArr = ImageSearch.FacesCapturedAt(time, cameID, true);//得到图片路径
            if (fileArr.Length == 0) return;

            for (int i = 0; i < fileArr.Length; ++i)
            {
                this.imageList1.Images.Add(Image.FromFile(fileArr[i]));
                string text = System.IO.Path.GetFileName(fileArr[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = fileArr[i].ToString(),
                    Text = text,
                    ImageIndex = i
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

        }

        private void picList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("图片路径："+this.picList.FocusedItem.Tag.ToString());
            ShowDetailPic(ImageDetail.FromPath(this.picList.FocusedItem.Tag.ToString()));
        }

        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
        }
    }
}
