using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using OpenCvSharp;
using ImageProcess;
using SuspectsRepository;

namespace FaceLibraryBuilder
{
    public partial class ImportPersonEnter : Form
    {

        SuspectsRepositoryManager mnger = new SuspectsRepositoryManager();

        public ImportPersonEnter()
        {
            InitializeComponent();
            InitCotrol(false);
        }



        string FileSavePath = @"c:\facerecognition\selectedface";
        string faceFeatureImagePath = @"c:\facerecognition\facesample";
        protected void InitCotrol(bool statu)
        {
            if (!statu)
            {
                txtAge.Text = "";
                txtCard.Text = "";
                txtId.Text = "";
                txtName.Text = "";
                rabMan.Checked = true;
                if (picTargetPerson.Image != null)
                {
                    picTargetPerson.Image.Dispose();
                    picTargetPerson.Image = null;
                }
            }
            txtAge.Enabled = statu;
            txtCard.Enabled = statu;
            txtId.Enabled = statu;
            txtName.Enabled = statu;
            rabMan.Enabled = statu;
            rabWoman.Enabled = statu;

            this.drawRectangle = Rectangle.Empty;
            this.picTargetPerson.Invalidate();
        }

        private void btnBrowseImpPerson_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "Jpeg 文件|*.jpg|Bmp 文件|*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string temp = ofd.FileName;
                    if (temp.EndsWith(".jpg") || temp.EndsWith(".bmp"))
                    {
                        string name = ofd.SafeFileName;
                        picTargetPerson.Image = Image.FromFile(temp);
                        picTargetPerson.Image.Tag = name;
                        InitCotrol(true);
                    }
                    else
                    {
                        MessageBox.Show("请选择以'.jpg'或者'.bmp'结尾的图片！", "提示");
                    }
                }
            }
        }

        private string ImageRepositoryDirectory
        {
            get
            {
                return this.directoryForImageRepository.Text;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.picTargetPerson.Image == null)
            {
                MessageBox.Show("请选定一张人脸图片");
                return;
            }

            if (drawRectangle == Rectangle.Empty)
            {
                MessageBox.Show("请定位人脸");
                return;
            }


            String oldFileName = this.picTargetPerson.Image.Tag as string;

            String fileName = System.Guid.NewGuid().ToString().ToUpper() + System.IO.Path.GetExtension(oldFileName);

            //搜索人脸
            OpenCvSharp.IplImage iplFace = BitmapConverter.ToIplImage((Bitmap)this.picTargetPerson.Image);

            const string ImageDirectory = "ImageRepository";

            string badGuyColorDirectory = Path.Combine(ImageRepositoryDirectory, ImageDirectory + @"\Bad\Color");
            if (!Directory.Exists(badGuyColorDirectory))
            {
                Directory.CreateDirectory(badGuyColorDirectory);
            }

            string badGuyColorFilePath = Path.Combine(badGuyColorDirectory, fileName);
            iplFace.SaveImage(badGuyColorFilePath);

            //归一化
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect(
                this.drawRectangle.X,
                this.drawRectangle.Y,
                this.drawRectangle.Width,
                this.drawRectangle.Height);

            OpenCvSharp.IplImage[] normalizedImages =
                Program.searcher.NormalizeImageForTraining(iplFace, rect);

            for (int i = 0; i < normalizedImages.Length; ++i)
            {
                string normalizedFaceName = string.Format("{0}_{1:d4}.jpg",
                    System.IO.Path.GetFileNameWithoutExtension(fileName), i);

                string badGuyGrayDirectory = System.IO.Path.Combine(ImageRepositoryDirectory,  ImageDirectory + @"\Bad\Gray");
                if (!Directory.Exists(badGuyGrayDirectory))
                {
                    Directory.CreateDirectory(badGuyGrayDirectory);
                }

                string grayFilePath = System.IO.Path.Combine(badGuyGrayDirectory, normalizedFaceName);

                normalizedImages[i].SaveImage(grayFilePath);
            }

            string id = txtId.Text.ToString();
            string name = txtName.Text.ToString();
            string sex = rabMan.Checked ? "男" : "女";
            int age = 0;

            int.TryParse(txtAge.Text, out age);

            string card = txtCard.Text.ToString();

            PersonInfo info = new PersonInfo();
            info.ID = id;
            info.Name = name;
            info.Sex = sex;
            info.Age = age;
            info.CardId = card;
            info.FileName = badGuyColorFilePath;
            info.Similarity = 0;

            mnger.Suspects.Add(info.FileName, info);

            MessageBox.Show("添加成功");

            Array.ForEach(normalizedImages, ipl => ipl.Dispose());

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        //generic pic filename
        protected string GetFileNameWithoutExtension()
        {
            DateTime dtime = DateTime.Now;
            string year = dtime.Year.ToString();
            return string.Format("{0}{1}{2}{3}{4}{5}{6}", year.Substring(2, 2),
                                                                    dtime.Month.ToString("d2"),
                                                                    dtime.Day.ToString("d2"),
                                                                    dtime.Hour.ToString("d2"),
                                                                    dtime.Minute.ToString("d2"),
                                                                    dtime.Second.ToString("d2"),
                                                                    dtime.Millisecond.ToString("d3"));
        }

        private void ImportPersonEnter_Load(object sender, EventArgs e)
        {

        }

        private int GetFaceSamplesCount()
        {
            string[] faceSamples = System.IO.Directory.GetFiles(faceFeatureImagePath, "*.jpg");
            return faceSamples.Length;
        }


        private void addFinished_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                this, 
                "确定生成人脸特征库？", 
                "请确认", 
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;


            if (GetFaceSamplesCount() < 40)
            {
                MessageBox.Show(this, "样本数量不足, 请继续添加", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            this.mnger.SaveTo( System.IO.Path.Combine( ImageRepositoryDirectory, "wanted.xml" ) );

//             FormProgress form = new FormProgress();
//             form.ShowDialog(this);


        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (string.IsNullOrEmpty(textbox.Text))
            {
                e.Cancel = true;
                this.errorProvider1.SetError(textbox, "不能为空");
            }
            else
                e.Cancel = false;
        }

        private void ImportPersonEnter_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string extenstion = System.IO.Path.GetExtension(files[0]);
                if (string.Compare(extenstion, ".jpg", false) == 0)
                {
                    InitCotrol(true);

                    Image img = Image.FromFile(files[0]);
                    img.Tag = files[0];
                    this.picTargetPerson.Image = img;
                    this.InitCotrol(true);
                    drawRectangle = Rectangle.Empty;

                }


            }

        }

        private void ImportPersonEnter_DragOver(object sender, DragEventArgs e)
        {
            bool isFile = e.Data.GetDataPresent(DataFormats.FileDrop);
            if (isFile)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        bool isDrag = false;
        Point startPoint;
        Rectangle theRectangle = Rectangle.Empty;
        Rectangle drawRectangle = Rectangle.Empty;
        Point positionPoint = Point.Empty;


        private void picTargetPerson_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.picTargetPerson.Image != null)
            {
                isDrag = true;
            }

            Control control = (Control)sender;
            startPoint = control.PointToScreen(new Point(e.X, e.Y));


        }

        private void picTargetPerson_MouseUp(object sender, MouseEventArgs e)
        {
            // If the MouseUp event occurs, the user is not dragging.
            isDrag = false;

            // Draw the rectangle to be evaluated. Set a dashed frame style 
            // using the FrameStyle enumeration.
            ControlPaint.DrawReversibleFrame(theRectangle,
                this.BackColor, FrameStyle.Dashed);

            Control ctrl = (Control)sender;

            drawRectangle = ctrl.RectangleToClient(NormalizeRectangle( theRectangle ));
            ctrl.Invalidate();

            theRectangle = new Rectangle(0, 0, 0, 0);


        }

        private void picTargetPerson_MouseMove(object sender, MouseEventArgs e)
        {

            // If the mouse is being dragged, 
            // undraw and redraw the rectangle as the mouse moves.
            if (isDrag)
            // Hide the previous rectangle by calling the 
            // DrawReversibleFrame method with the same parameters.
            {
                ControlPaint.DrawReversibleFrame(theRectangle,
                    this.BackColor, FrameStyle.Dashed);

                Control ctrl = (Control)sender;

                // Calculate the endpoint and dimensions for the new 
                // rectangle, again using the PointToScreen method.
                Point endPoint = ctrl.PointToScreen(new Point(e.X, e.Y));
                int width = endPoint.X - startPoint.X;
                int height = endPoint.Y - startPoint.Y;
                theRectangle = new Rectangle(startPoint.X,
                    startPoint.Y, width, height);

                // Draw the new rectangle by calling DrawReversibleFrame
                // again.  
                ControlPaint.DrawReversibleFrame(theRectangle,
                    this.BackColor, FrameStyle.Dashed);
            }

        }

        private static Rectangle NormalizeRectangle(Rectangle rc)
        {
            int x = rc.Left;
            int width = Math.Abs(rc.Width);
            int height = Math.Abs(rc.Height);

            if (rc.Width < 0)
            {
                x -= width;
            }

            int y = rc.Top;
            if (rc.Height < 0)
            {
                y -= height;
            }

            return new Rectangle(x, y, width, height);
        }
        private void picTargetPerson_Paint(object sender, PaintEventArgs e)
        {
            if (this.drawRectangle.Size != Size.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Black, drawRectangle);
            }

        }

        private void browseForDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            this.directoryForImageRepository.Text = this.folderBrowserDialog1.SelectedPath;
            this.btnAdd.Enabled = true;
            this.addFinished.Enabled = true;
        }


    }
}