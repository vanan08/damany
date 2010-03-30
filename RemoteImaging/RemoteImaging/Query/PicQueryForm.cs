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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form, IPicQueryScreen
    {

        public PicQueryForm()
        {
            InitializeComponent();
            this.PageSize = 20;
        }

        public void AttachPresenter(IPicQueryPresenter presenter)
        {
            this.presenter = presenter;

        }

        public void SetCameras(IList<Damany.PC.Domain.CameraInfo> cameras)
        {

            if (cameras == null)
                throw new ArgumentNullException("cameras", "cameras is null.");

            foreach (var camera in cameras)
            {
                this.cameraIdCombo.Items.Add(camera.Id.ToString());
            }

        }

        private void ShowUserError(string msg)
        {
            MessageBox.Show(this,
                                msg,
                                this.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
            facesListView.BeginUpdate();

            ClearCurPageList();

            for (int i = (currentPage - 1) * PageSize;
                (i < currentPage * PageSize) && (i < imagesFound.Length);
                ++i)
            {
                Image img = null;
                try
                {
                    img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(imagesFound[i]);
                }
                catch (System.IO.IOException ex)
                {
                    bool rethrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionHandlingLogging);
                    if (rethrow)
                    {
                        throw;
                    }

                    continue;
                }

                this.faceImageList.Images.Add(img);
                string text = System.IO.Path.GetFileName(imagesFound[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = imagesFound[i],
                    Text = text,
                    ImageIndex = i % PageSize
                };
                this.facesListView.Items.Add(item);
            }

            facesListView.EndUpdate();

        }

        private void ClearCurPageList()
        {
            this.facesListView.Clear();
            this.faceImageList.Images.Clear();
        }

        private void ClearLists()
        {
            ClearCurPageList();
            this.imageList2.Images.Clear();
            this.currentFace.Image = null;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.presenter.Search();
        }


        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            string filePath = this.facesListView.FocusedItem.Tag as string;

            try
            {
                this.currentFace.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(filePath);

                //detail infomation
                ImageDetail imgInfo = ImageDetail.FromPath(filePath);

                string captureLoc = string.Format("抓拍地点: {0}", imgInfo.FromCamera);
                this.labelCaptureLoc.Text = captureLoc;

                string captureTime = string.Format("抓拍时间: {0}", imgInfo.CaptureTime);
                this.labelCaptureTime.Text = captureTime;

                string bigImgPath = FileSystemStorage.BigImgPathForFace(imgInfo);

                this.wholePicture.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(bigImgPath);
            }
            catch (System.IO.IOException ex)
            {
                ShowUserError(ex.Message);
            }

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
                this.imageList2.Images.Add(Damany.Util.Extensions.MiscHelper.FromFileBuffered(files[i]));
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
            this.facesListView.Clear();
            this.faceImageList.Images.Clear();
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

        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            if (this.facesListView.SelectedItems.Count != 1) return;

            string imgPath = this.facesListView.SelectedItems[0].Tag as string;

            ImageDetail imgInfo = ImageDetail.FromPath(imgPath);

            string[] videos = FileSystemStorage.VideoFilesOfImage(imgInfo);

            if (videos.Length == 0)
            {
                MessageBox.Show("未找到相关视频");
                return;
            }

            VideoPlayer.PlayVideosAsync(videos);

        }

        private void SaveSelectedImage()
        {
            if ((this.facesListView.Items.Count <= 0) || (this.facesListView.FocusedItem == null)) return;
            string filePath = this.facesListView.FocusedItem.Tag as string;

            if (File.Exists(filePath))
            {
                this.currentFace.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(filePath);
            }
            ImageDetail imgInfo = ImageDetail.FromPath(filePath);
            string bigImgPath = FileSystemStorage.BigImgPathForFace(imgInfo);

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.RestoreDirectory = true;
                saveDialog.Filter = "Jpeg 文件|*.jpg";
                //saveDialog.FileName = filePath.Substring(filePath.Length - 27, 27);
                string fileName = Path.GetFileName(filePath);
                saveDialog.FileName = fileName;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (currentFace.Image != null)
                    {
                        string path = saveDialog.FileName;
                        currentFace.Image.Save(path);
                        path = path.Replace(fileName, Path.GetFileName(bigImgPath));
                        wholePicture.Image.Save(path);
                    }
                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveSelectedImage();
        }



        public int PageSize { get; set; }

        private string[] imagesFound = new string[0];
        private int currentPage;
        private int totalPage;
        private IPicQueryPresenter presenter;



        #region IPicQueryScreen Members


        public void Clear()
        {
            this.facesListView.Items.Clear();
            this.faceImageList.Images.Clear();
            this.currentFace.Image = null;
            this.wholePicture.Image = null;
        }

        public void AddItem(Damany.Imaging.Common.Portrait item)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action = this.AddItem;
                this.BeginInvoke(action, item);
                return;
            }

            this.faceImageList.Images.Add(item.GetImage().ToBitmap());

            var lvi = new ListViewItem
            {
                Tag = item,
                Text = item.CapturedAt.ToString(),
                ImageIndex = this.faceImageList.Images.Count - 1
            };

            this.facesListView.Items.Add(lvi);
        }

        public void EnableSearchButton(bool enable)
        {
            if (InvokeRequired)
            {
                Action<bool> action = this.EnableSearchButton;
                this.BeginInvoke(action, enable);
                return;
            }

            this.queryBtn.Enabled = enable;
        }

        public void EnableNavigateButtons(bool enable)
        {
            if (InvokeRequired)
            {
                Action<bool> action = this.EnableNavigateButtons;
                this.BeginInvoke(action, enable);
                return;
            }
            

            this.toolStripButtonFirstPage.Enabled = enable;
            this.toolStripButtonPrePage.Enabled = enable;
            this.toolStripButtonNextPage.Enabled = enable;
            this.toolStripButtonLastPage.Enabled = enable;
        }

        public Damany.Util.DateTimeRange TimeRange
        {
            get
            {
                return new Damany.Util.DateTimeRange(
                    (DateTime) this.timeFrom.EditValue,
                    (DateTime) this.timeTo.EditValue
                    );
            }
            set
            {
                
            }
        }

        public Damany.Imaging.Common.Portrait SelectedItem
        {
            get
            {
                if (this.facesListView.SelectedItems.Count <= 0) return null;

                return this.facesListView.SelectedItems[0].Tag as Damany.Imaging.Common.Portrait;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Damany.PC.Domain.Destination SelectedCamera
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public Damany.PC.Domain.CameraInfo[] Cameras
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.cameraIdCombo.DataSource = value;
                this.cameraIdCombo.DisplayMember = "Id";
            }
        }

        public string[] Machines
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.machineCombo.DataSource = value;
            }
        }

        public void Show()
        {
            this.ShowDialog();
        }

        #endregion
    }
}
