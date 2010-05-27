using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteImaging.Query
{
    public partial class QueryForm : Form
    {
        public QueryForm()
        {
            InitializeComponent();
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            string cameraID = this.comboBox1.Text;

            string dataStr1 = this.dateTimePicker1.Text;
            int indexYear1 = dataStr1.IndexOf("年");
            int indexMonth1 = dataStr1.IndexOf("月");
            int indexDay1 = dataStr1.IndexOf("日");

            string year1 = dataStr1.Substring(0,4);
            string month1 = int.Parse(dataStr1.Substring(indexYear1+1, indexMonth1-indexYear1-1)).ToString("D2");
            string day1 = int.Parse(dataStr1.Substring(indexMonth1+1,indexDay1-indexMonth1-1)).ToString("D2");

            string timeStr1 = this.timeEdit1.Text;
            int index1 = timeStr1.IndexOf(":");
            int index2 = timeStr1.LastIndexOf(":");

            string hour1 = timeStr1.Substring(0,index1);
            string minute1 = timeStr1.Substring(index1+1,index2-index1-1);
            string second1 = timeStr1.Substring(index2+1,timeStr1.Length-index2-1);

            string dataStr2 = this.dateTimePicker2.Text;
            int indexYear2 = dataStr2.IndexOf("年");
            int indexMonth2 = dataStr2.IndexOf("月");
            int indexDay2 = dataStr2.IndexOf("日");

            string year2 = dataStr2.Substring(0, 4);
            string month2 = int.Parse(dataStr2.Substring(indexYear2 + 1, indexMonth2 - indexYear2 - 1)).ToString("D2");
            string day2 = int.Parse(dataStr2.Substring(indexMonth2 + 1, indexDay2 - indexMonth2 - 1)).ToString("D2");

            string timeStr2 = this.timeEdit2.Text;
            int index3 = timeStr2.IndexOf(":");
            int index4 = timeStr2.LastIndexOf(":");

            string hour2 = timeStr2.Substring(0, index3);
            string minute2 = timeStr2.Substring(index3 + 1, index4 - index3 - 1);
            string second2 = timeStr2.Substring(index4 + 1, timeStr2.Length - index4 - 1);

            //judge the input validation
            DateTime dateTime1 = new DateTime(int.Parse(year1),int.Parse(month1),int.Parse(day1),int.Parse(hour1),int.Parse(minute1),int.Parse(second1));
            DateTime dateTime2 = new DateTime(int.Parse(year2), int.Parse(month2), int.Parse(day2), int.Parse(hour2), int.Parse(minute2), int.Parse(second2));
            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！","警告");
                return;
            }
            //

            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();

            Query.ImageDirSys startDir = new ImageDirSys(cameraID, year1, month1, day1, hour1, minute1, second1);
            Query.ImageDirSys endDir = new ImageDirSys(cameraID, year2, month2, day2, hour2, minute2, second2);
            Query.ImageSearch imageSearch = new ImageSearch();

            string[] files = imageSearch.SearchImages(startDir, endDir, RemoteImaging.Query.ImageDirSys.BeginDir);
            if (files == null)
            {
                MessageBox.Show("没有搜索到满足条件的图片！","警告");
                return;
            }

            this.bestPicListView.Scrollable = true;
            this.bestPicListView.MultiSelect = false;
            this.bestPicListView.View = View.LargeIcon;
            this.bestPicListView.LargeImageList = imageList1;

            for (int i = 0; i < files.Length; i++)
            {
                this.imageList1.Images.Add(Image.FromFile(files[i]));
                this.bestPicListView.Items.Add(System.IO.Path.GetFileName(files[i]), i);
            }

        }

        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            this.secPicListView.Clear();
            this.imageList2.Images.Clear();

            Query.ImageSearch imageSearch = new ImageSearch();
            string[] files = imageSearch.SelectedBestImageChanged(this.bestPicListView.FocusedItem.Text, RemoteImaging.Query.ImageDirSys.BeginDir);
            this.secPicListView.Scrollable = true;
            this.secPicListView.MultiSelect = false;
            this.secPicListView.View = View.LargeIcon;
            this.secPicListView.LargeImageList = imageList2;

            for (int i = 0; i < files.Length; i++)
            {
                this.imageList2.Images.Add(Image.FromFile(files[i]));
                this.secPicListView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(files[i]), i);
            }

            string filePath = RemoteImaging.Query.ImageDirSys.BeginDir + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(0, 2) + "\\" +
                              (2000 + int.Parse(this.bestPicListView.FocusedItem.Text.Substring(3, 2))).ToString() + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(5, 2) + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(7, 2) + "\\" + Query.ImageDirSys.IconPath + "\\" + this.bestPicListView.FocusedItem.Text;

            //show modify icon
            if (File.Exists(filePath))
            {
                this.pictureBox1.Image = Image.FromFile(filePath);
            }
            //

            //detail infomation
            Camera camera = new Camera();
            camera.ID = 01;
            camera.Name = "四川大学南大门摄像头";
            this.gotPlaceTxt.Text = "四川大学南大门摄像头";

            string focusedFileName = this.bestPicListView.FocusedItem.Text;
            this.gotTimeTxt.Text = (2000 + int.Parse(focusedFileName.Substring(3, 2))).ToString() + "年" + //year
                                   focusedFileName.Substring(5, 2) + "月" + //month
                                   focusedFileName.Substring(7, 2) + "日" + //day
                                   focusedFileName.Substring(9, 2) + "时" + //hour
                                   focusedFileName.Substring(11, 2) + "分" + //minute
                                   focusedFileName.Substring(13, 2) + "妙";//second

            //
        }

        private void bestPicListView_DoubleClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show("double click");
        }

        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {
            string filePath = RemoteImaging.Query.ImageDirSys.BeginDir + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(0, 2) + "\\" +
                              (2000 + int.Parse(this.secPicListView.FocusedItem.Text.Substring(3, 2))).ToString() + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(5, 2) + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(7, 2) + "\\" + Query.ImageDirSys.BeginDir + "\\" + this.secPicListView.FocusedItem.Text;

            if (File.Exists(filePath))
            {
                this.pictureBox1.Image = Image.FromFile(filePath);
            }

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
