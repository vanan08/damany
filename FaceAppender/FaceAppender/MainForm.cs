using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IniParser;
using Timer = System.Timers.Timer;

namespace FaceAppender
{
    public partial class MainForm : Form
    {
        FileIniDataParser _parser = new FileIniDataParser();
        private int _count = 0;
        private Timer _timer;

        public MainForm()
        {
            InitializeComponent();

            _timer = new Timer(1000);
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            Process();
            _timer.Start();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = this.Text;
                notifyIcon1.BalloonTipText = "融合器以后台方式运行，单击图标可以重新打开主窗口";
                notifyIcon1.ShowBalloonTip(3000);
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void browseForSrcDir_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SourceDir = BrowseForDirectory();
        }


        private string BrowseForDirectory()
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                return folderBrowserDialog1.SelectedPath;
            }

            return null;
        }

        private void browseForDstDir_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DestDir = BrowseForDirectory();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
            notifyIcon1.Visible = false;
        }

        public static Image CombineImages(Image baseImage, IEnumerable<Image> zoomImages)
        {
            if (zoomImages.Count() == 0) return baseImage;

            var delta = CalculateWidhDelta(zoomImages);
            var img = new Bitmap(baseImage.Width + delta, baseImage.Height);
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.LightBlue);
                g.DrawImage(baseImage, 0, 0);

                var bp = new Point(baseImage.Width, 0);
                foreach (var faceImage in zoomImages)
                {
                    var ration = (float)faceImage.Height / faceImage.Width;
                    var h = (int)(delta * ration);
                    DrawImage(g, faceImage, new Rectangle(bp.X, bp.Y, delta, h));
                    bp.Y += h + 5;
                }
            }

            return img;
        }

        private static int CalculateWidhDelta(IEnumerable<Image> images)
        {
            if (images.Count() == 0) return 0;

            var max = images.Max(i => i.Width);
            if (max < 250 || max > 400)
            {
                max = 250;
            }
            return max;
        }

        private static void DrawImage(Graphics g, Image img, Rectangle destRectangle)
        {
            var borderWidth = 5;

            destRectangle.Inflate(-2, -2);
            g.FillRectangle(Brushes.White, destRectangle);
            destRectangle.Inflate(-3, -3);
            g.DrawImage(img, destRectangle);
            destRectangle.Inflate(borderWidth, borderWidth);
        }

        private void Process()
        {
            if (!Directory.Exists(Properties.Settings.Default.SourceDir)
                || !Directory.Exists(Properties.Settings.Default.DestDir))

                return;

            var iniFiles = ScanIniFiles();
            foreach (var iniFile in iniFiles)
            {
                try
                {
                    ProcessIniFile(iniFile);
                }
                catch (IOException)
                {
                    //var destDir = Path.Combine(Properties.Settings.Default.SourceDir, "BadFormat");
                    //if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);

                    //var destPath = Path.Combine(destDir, Path.GetFileName(iniFile));
                    //File.Move(iniFile, destPath);
                }
            }

        }

        private void ProcessIniFile(string iniFile)
        {
            UpdateCurrentIniLabel(iniFile);

            var parseResult = ParseIniFile(iniFile);
            var destDir = Properties.Settings.Default.DestDir;
            var sourceDir = Properties.Settings.Default.SourceDir;
            var allExist = parseResult.ImageFiles.All(f => File.Exists(Path.Combine(sourceDir, f)));
            if (!allExist) return;

            MoveImageFiles(parseResult, destDir, sourceDir);
            var iniDstPath = Path.Combine(destDir, Path.GetFileName(iniFile));
            File.Move(iniFile, iniDstPath);

            UpdateCounter();
        }

        private void MoveImageFiles(ParseResult parseResult, string destDir, string srcDir)
        {
            for (int i = 0; i < parseResult.ImageFiles.Length; i++)
            {
                var imgName = parseResult.ImageFiles[i];
                if (i == parseResult.ImageIndex)
                {
                    var imgPath = Path.Combine(Properties.Settings.Default.SourceDir, imgName);
                    var baseImg = AForge.Imaging.Image.FromFile(imgPath);
                    var faceImgs = ExtractSubImages(baseImg, parseResult.FaceRectangles);
                    var lprImg = ExtractSubImages(baseImg, new Rectangle[] { parseResult.PlateRectangle });

                    var totalImages = new List<Image>(faceImgs);
                    totalImages.AddRange(lprImg);
                    var combinedImg = CombineImages(baseImg, totalImages);

                    //save combined new image, and delete old image file;
                    var destPath = Path.Combine(destDir, imgName);
                    combinedImg.Save(destPath);
                    var originalPath = Path.Combine(srcDir, imgName);
                    File.Delete(originalPath);

                    UpdatePictureBox(combinedImg);

                    combinedImg.Dispose();
                    totalImages.ForEach(img => img.Dispose());
                }
                else
                {
                    var srcImgPath = Path.Combine(srcDir, imgName);
                    var destImgPath = Path.Combine(destDir, imgName);
                    File.Move(srcImgPath, destImgPath);
                }
            }
        }


        private Image[] ExtractSubImages(Image baseImg, IEnumerable<Rectangle> rectangles)
        {
            var images = rectangles.Select(rectangle => ExtractSubimage(baseImg, rectangle)).ToList();
            return images.ToArray();
        }

        private Image ExtractSubimage(Image image, Rectangle rectangle)
        {
            var img = new Bitmap(rectangle.Width, rectangle.Height);
            using (var g = Graphics.FromImage(img))
            {
                g.DrawImage(image, 0, 0, rectangle, GraphicsUnit.Pixel);
            }

            return img;
        }

        private ParseResult ParseIniFile(string iniFile)
        {
            const string carSection = "Car";
            const string faceSection = "Face";

            var iniData = _parser.LoadFile(iniFile);

            var images = new List<string>();
            var idx = 1;
            while (true)
            {
                var img = iniData[carSection]["Img" + idx.ToString()];
                if (string.IsNullOrEmpty(img)) break;

                images.Add(img);
                ++idx;
            }

            var imgIndex = int.Parse(iniData[faceSection]["ImgIndex"]) - 1;


            var px = int.Parse(iniData[faceSection]["PlateX"]);
            var py = int.Parse(iniData[faceSection]["PlateY"]);
            var pw = int.Parse(iniData[faceSection]["PlateWidth"]);
            var ph = int.Parse(iniData[faceSection]["PlateHeight"]);

            var faces = new List<Rectangle>();
            var faceCount = int.Parse(iniData[faceSection]["Count"]);
            for (int i = 0; i < faceCount; i++)
            {
                var fx = int.Parse(iniData[faceSection]["FaceX" + (i + 1).ToString()]);
                var fy = int.Parse(iniData[faceSection]["FaceY" + (i + 1).ToString()]);
                var fw = int.Parse(iniData[faceSection]["FaceWidth" + (i + 1).ToString()]);
                var fh = int.Parse(iniData[faceSection]["FaceHeight" + (i + 1).ToString()]);

                faces.Add(new Rectangle(fx, fy, fw, fh));
            }

            var lprRect = new Rectangle(px, py, pw, ph);
            lprRect.Inflate(20, 20);

            var result = new ParseResult()
                             {
                                 FaceRectangles = faces.ToArray(),
                                 ImageFiles = images.ToArray(),
                                 ImageIndex = imgIndex,
                                 PlateRectangle = lprRect
                             };

            return result;
        }

        private IEnumerable<string> ScanIniFiles()
        {
            return Directory.EnumerateFiles(Properties.Settings.Default.SourceDir, "*.ini");
        }


        private void UpdatePictureBox(Image combinedImg)
        {
            var newImg = (Image)combinedImg.Clone();
            Action doer = () =>
                              {
                                  var old = lastPicture.Image;
                                  lastPicture.Image = newImg;
                                  if (old != null)
                                      old.Dispose();

                              };
            this.BeginInvoke(doer);
        }

        private void UpdateCounter()
        {
            ++_count;
            Action doer = () => labelCounter.Text = string.Format("已处理:{0}", _count);
            this.BeginInvoke(doer);
        }

        private void UpdateCurrentIniLabel(string iniFile)
        {
            Action ac = () => labelCurrentIni.Text = iniFile;
            this.BeginInvoke(ac);
        }
        class ParseResult
        {
            public string[] ImageFiles { get; set; }
            public int ImageIndex { get; set; }
            public Rectangle[] FaceRectangles { get; set; }
            public Rectangle PlateRectangle { get; set; }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var big = Image.FromFile(@"d:\lprbig.jpg");

            var f1 = Image.FromFile(@"d:\02_090505085314-0001-crop.jpg");
            var f2 = Image.FromFile(@"d:\Image0002-crop2.jpg");
            var f3 = Image.FromFile(@"d:\lpr.jpg");

            var combined = CombineImages(big, new[] { f1, f2, f3 });
            combined.Save(@"d:\temp.jpg");
        }
    }
}
