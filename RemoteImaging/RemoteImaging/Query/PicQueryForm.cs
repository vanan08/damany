using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Damany.PC.Domain;
using Damany.RemoteImaging.Common;
using RemoteImaging.Core;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form, IPicQueryScreen
    {
        private readonly FileSystemStorage _videoRepository;
		private readonly ConfigurationManager _manager;

        public PicQueryForm(FileSystemStorage videoRepository, ConfigurationManager manager)
        {
            _videoRepository = videoRepository;
            _manager = manager;

            InitializeComponent();

            this.timeFrom.EditValue = DateTime.Now.AddDays(-1);
            this.timeTo.EditValue = DateTime.Now;

            this.facesListView.LargeImageList = this.faceImageList;
            this.facesListView.SelectedIndexChanged += new EventHandler(facesListView_SelectedIndexChanged);

            this.pageSizeCombo.ComboBox.DataSource = new int[] { 20, 30, 40, 50 };
            this.pageSizeCombo.ComboBox.SelectedItem = 20;

            this.PageSize = 20;
        }
		


        void facesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.SelectedItemChanged();
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
            if (InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(this.UpdatePagesLabel));
                return;
            }

            this.toolStripLabelCurPage.Text = string.Format("第{0}/{1}页", currentPage, totalPage);
        }

       
        private void ClearCurPageList()
        {
            this.facesListView.Clear();
            this.faceImageList.Images.Clear();
        }

        private void ClearLists()
        {
            this.imageList2.Images.Clear();
            this.currentFace.Image = null;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.presenter.Search();
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
            
        }

        private void toolStripButtonFirstPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToFirst();

        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToPrev();

        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToNext();
        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToLast();
        }

        private void toolStripComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageSize = (int)this.pageSizeCombo.SelectedItem;

            if (presenter != null)
            {
                this.presenter.PageSizeChanged();
            }

            

        }

        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            presenter.PlayVideo();

        }

        private void SaveSelectedImage()
        {
            if ((this.facesListView.Items.Count <= 0) || (this.facesListView.FocusedItem == null)) return;
            var portrait = this.facesListView.FocusedItem.Tag as Damany.Imaging.Common.Portrait;

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.RestoreDirectory = true;
                saveDialog.Filter = "Jpeg 文件|*.jpg";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    portrait.GetIpl().SaveImage(saveDialog.FileName);

                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveSelectedImage();
        }


        public int PageSize 
        { 
            get
            {
                return this.pageSize;
            }
            set
            {
                
                //this.pageSizeCombo.SelectedItem = value;
                this.pageSize = value;
            }
        }

        private string[] imagesFound = new string[0];
        private int currentPage;
        private int totalPage;
        private int pageSize;
        private IPicQueryPresenter presenter;


        #region IPicQueryScreen Members


        public void Clear()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Clear));
                return;
            }

            this.facesListView.Items.Clear();
            this.faceImageList.Images.Clear();
        }

        public void AddItem(Damany.Imaging.Common.Portrait item)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action = this.AddItem;
                this.BeginInvoke(action, item);
                return;
            }

            this.faceImageList.Images.Add(item.GetIpl().ToBitmap());

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
                if (cameraIdCombo.SelectedValue == null)
                {
                    return null;
                }

                CameraInfo cam = (CameraInfo) cameraIdCombo.SelectedValue;
                int id =  cam.Id;

                return new Destination()
                           {
                               CameraId = id,
                               MachineName = (string) (machineCombo.SelectedValue ?? string.Empty)
                           };
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
                this.cameraIdCombo.DisplayMember = "Name";
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

        public void ShowUserIsBusy(bool busy)
        {
            Cursor.Current = busy ? Cursors.WaitCursor : Cursors.Default;
        }


        public Damany.Imaging.Common.Portrait CurrentPortrait
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.currentFace.Image = value.GetIpl().ToBitmap();
                this.captureLocation.Text = "抓拍地点：" + _manager.GetName(value.CapturedFrom.Id) ?? "未知";
                this.captureTime.Text = "抓拍时间：" + value.CapturedAt.ToString();
            }
        }

        public Image CurrentBigPicture
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.wholePicture.Image = value;
            }
        }


        public int CurrentPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.currentPage = value;
                this.UpdatePagesLabel();
                
            }
        }

        public int TotalPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.totalPage = value;
                this.UpdatePagesLabel();
            }
        }


        public void ShowStatus(string status)
        {
            if (InvokeRequired)
            {
                Action<string> action = this.ShowStatus;

                this.BeginInvoke(action, status);
                return;
            }

            this.status.Text = status;
        }

        #endregion

        #region IPicQueryScreen Members


        public string SelectedMachine
        {
            get
            {
                return  (string) this.machineCombo.SelectedValue;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
