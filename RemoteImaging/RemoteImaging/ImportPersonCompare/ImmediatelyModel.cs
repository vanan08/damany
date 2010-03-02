using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RemoteImaging.Query;
using FaceRecognition;

namespace RemoteImaging.ImportPersonCompare
{
    public partial class ImmediatelyModel : Form
    {
        public ImmediatelyModel()
        {
            InitializeComponent();
            btnOK.Enabled = false;
        }

        private void PersonCheck_Load(object sender, EventArgs e)
        {

        }

        public ImageDirSys SetWarnInfo
        {
            set
            {
                if (value != null)
                {
                    lblDate.Text = string.Format("日期： {0}-{1}-{2}", value.Year, value.Month, value.Day);
                    lblTime.Text = string.Format("时间： {0}:{1}:{2}", value.Hour, value.Minute, value.Second);
                    lblAddress.Text = string.Format("地址： {0}", value.CameraID.ToString());
                }
            }
        }

        //private string time = "";
        //public string TimeSet
        //{
        //    private get { return time; }
        //    set { lblTime.Text = string.Format("时间： {0}", value.ToString()); }
        //}

        //private string day = "";
        //public string DateSet
        //{
        //    private get { return day; }
        //    set { lblDate.Text = string.Format("日期： {0}", value.ToString()); }
        //}

        //private string address = "";
        //public string AddressSet
        //{
        //    private get { return address; }
        //    set { lblAddress.Text = string.Format("地址： {0}", value.ToString()); }
        //}

        //private string picCheckFilePath = "";
        ///// <summary>
        ///// 通过路径加载待识别图片
        ///// </summary>
        //public string PicCheckFilePath
        //{
        //    private get { return picCheckFilePath; }
        //    set { picCheck.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(value.ToString()); }
        //}

        /// <summary>
        /// 通过内存加载图片
        /// </summary>
        private Image picCheckImg = null;
        public Image PicCheckImg
        {
            private get { return picCheckImg; }
            set { picCheck.Image = (Image)value; }
        }

        private List<ImportantPersonDetail> listPersons = null;
        public List<ImportantPersonDetail> ShowPersons
        {
            private get { return listPersons; }
            set
            {
                this.listPersons = value;

                InitControl(listPersons);
            }
        }

        private static int CompareTarget(ImportantPersonDetail x, ImportantPersonDetail y)
        {
            //similarity is in percent float.
            return (int)(y.Similarity.Similarity * 1000 - x.Similarity.Similarity * 1000);
        }

        protected void InitControl(List<ImportantPersonDetail> listpersons)
        {
            listpersons.Sort(CompareTarget);

            foreach (ImportantPersonDetail ipd in listPersons)
            {

                //                     float x = Convert.ToSingle(p[0]);
                //                     float y = Convert.ToSingle(p[1]);

                ListViewItem lvi = new ListViewItem(new string[] { "",
                                                            ipd.Info.Name,
                                                            ipd.Info.Sex.ToString(),
                                                            ipd.Info.Age.ToString(),
                                                            ipd.Info.CardId,
                                                            string.Empty});
                lvi.SubItems[0].Tag = ipd.Similarity; //人脸库中的图片
                lvi.SubItems[1].Tag = ipd.Info.FileName;//犯罪分子图片  未进行灰度图转换
                this.v.Items.Add(lvi);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //将比对好的图片 另外存入一个文件夹中
            if (v.FocusedItem == null)
            {
                MessageBox.Show("请选择报警信息！", "警告");
                return;
            }
            bool res = false;
            if (MessageBox.Show("确定要保存当前选择的报警信息？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                res = true;
            else
                return;

            if (res)
                SaveCurrentSelectedInfo();
        }

        private void SaveCurrentSelectedInfo()
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string path = Path.Combine(dlg.SelectedPath, GetFileName());
                    picCheck.Image.Save(path);
                }
            }
        }

        //generic pic filename
        protected string GetFileName()
        {
            DateTime dtime = DateTime.Now;
            string year = dtime.Year.ToString();
            return string.Format("{0}{1}{2}{3}{4}{5}{6}.jpg", year.Substring(2, 2),
                                                                    dtime.Month.ToString("d2"),
                                                                    dtime.Day.ToString("d2"),
                                                                    dtime.Hour.ToString("d2"),
                                                                    dtime.Minute.ToString("d2"),
                                                                    dtime.Second.ToString("d2"),
                                                                    dtime.Millisecond.ToString("d3"));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //取消时 删除模板库中的 动态比对图片
            this.Close();
            this.Dispose();
        }

        private void lvPersonInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (v.SelectedItems.Count > 0)
            {
                ListViewItem lvi = v.SelectedItems[0];
                RecognizeResult sm = (RecognizeResult)lvi.SubItems[0].Tag;
                string range = lvi.SubItems[5].Text;
                lblTextSim.Text = string.Format("相似度: {0:F0}%", sm.Similarity * 100);
                //犯罪分子图片显示
                if (picStandard.Image != null)
                {
                    picStandard.Image.Dispose();
                    picStandard.Image = null;
                }

                string path = Path.Combine(Properties.Settings.Default.ImpSelectPersonPath, lvi.SubItems[1].Tag as string);


                picStandard.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(path);
                btnOK.Enabled = true;
            }
        }

        private void ImmediatelyModel_Shown(object sender, EventArgs e)
        {
            if (this.v.Items.Count > 0)
            {
                this.v.Items[0].Selected = true;
                this.v.Select();
            }

        }
    }


}
