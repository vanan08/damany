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
        }

        public static Image CombineFaceAndLpr(Image baseImage, IEnumerable<Image> faceImages, IEnumerable<Image> lprImages)
        {
            int faceWidth = faceImages.Count() > 0 ? faceImages.Max(f => f.Width) : 0;
            int faceHeight = faceImages.Count() > 0 ? faceImages.Max(f => f.Height) : 0;
            var rectangleMargin = new Padding(10);
            var faceXSpace = 10;
            var faceYSpace = 10;
            var column = 1;
            var bigLprWidth = 0;
            var fillColor = Color.LightBlue;

            if (lprImages != null)
            {
                bigLprWidth = lprImages.Max(i => i.Width);
            }

            var widthIncrement = Math.Max((faceWidth * column + faceXSpace + rectangleMargin.Horizontal), bigLprWidth + rectangleMargin.Horizontal);

            var img = new Bitmap(baseImage.Width + widthIncrement, baseImage.Height);
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(fillColor);
                g.DrawImage(baseImage, 0, 0);

                var bp = new Point(baseImage.Width + rectangleMargin.Left, rectangleMargin.Top);
                var faceArray = faceImages.ToArray();

                for (int i = 0; i < faceArray.Length; i++)
                {
                    var l = i / column;
                    var c = i % column;
                    DrawFace(faceWidth, faceHeight, faceXSpace, faceYSpace, column, g, bp, faceArray, l, c);
                }

                if (lprImages != null)
                {
                    var y = baseImage.Height - rectangleMargin.Bottom;

                    foreach (var lprImage in lprImages)
                    {
                        var x = baseImage.Width + (widthIncrement - lprImage.Width) / 2;
                        y -= lprImage.Height + faceYSpace;
                        g.DrawImage(lprImage, x, y);
                    }
                }

            }

            return img;
        }

        private static void DrawFace(int faceWidth, int faceHeight, int faceXSpace, int faceYSpace, int column, Graphics g, Point bp, Image[] faceArray, int l, int c)
        {
            var r = new Rectangle(bp.X + (faceWidth + faceXSpace) * c, bp.Y + (faceHeight + faceYSpace) * l,
                                                          faceWidth, faceHeight);

            //draw shadow
            var shadowDistance = 2;
            r.Inflate(3, 3);
            g.FillRectangle(Brushes.White, r);
            r.Inflate(-3, -3);

            g.DrawImage(faceArray[l * column + c], r);
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
                catch (System.IO.IOException)
                {
                    var destDir = Path.Combine(Properties.Settings.Default.SourceDir, "BadFormat");
                    if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);

                    var destPath = Path.Combine(destDir, Path.GetFileName(iniFile));
                    File.Move(iniFile, destPath);
                }
            }

        }

        private void ProcessIniFile(string iniFile)
        {
            Action ac = () => labelCurrentIni.Text = iniFile;
            this.BeginInvoke(ac);

            var parseResult = ParseIniFile(iniFile);

            Image combinedImg = null;
            if (parseResult.ImageIndex != -1)
            {
                var imgName = parseResult.ImageFiles[parseResult.ImageIndex];
                var imgPath = Path.Combine(Properties.Settings.Default.SourceDir, imgName);
                var baseImg = AForge.Imaging.Image.FromFile(imgPath);
                var faceImgs = ExtractFaces(baseImg, parseResult.FaceRectangles);
                var lprImg = ExtractFaces(baseImg, new Rectangle[] { parseResult.PlateRectangle });

                combinedImg = CombineFaceAndLpr(baseImg, faceImgs, lprImg);
            }

            var destDir = Properties.Settings.Default.DestDir;
            var srcDir = Properties.Settings.Default.SourceDir;

            for (int i = 0; i < parseResult.ImageFiles.Length; i++)
            {
                var imgName = parseResult.ImageFiles[i];
                if (i == parseResult.ImageIndex)
                {

                    //save combined new image, and delete old image file;
                    var destPath = Path.Combine(destDir, imgName);
                    combinedImg.Save(destPath);

                    var originalPath = Path.Combine(srcDir, imgName);
                    File.Delete(originalPath);

                    var oldImg = lastPicture.Image;
                    var newImg = (Image)combinedImg.Clone();
                    ac = () => lastPicture.Image = newImg;
                    this.BeginInvoke(ac);
                    if (oldImg != null)
                    {
                        oldImg.Dispose();
                    }

                    combinedImg.Dispose();
                }
                else
                {
                    var srcImgPath = Path.Combine(srcDir, imgName);
                    var destImgPath = Path.Combine(destDir, imgName);
                    File.Move(srcImgPath, destImgPath);
                }
            }

            var iniDstPath = Path.Combine(destDir, Path.GetFileName(iniFile));
            File.Move(iniFile, iniDstPath);


            ++_count;
            ac = () => labelCounter.Text = string.Format("已处理:{0}", _count);
            this.BeginInvoke(ac);
        }

        private Image[] ExtractFaces(Image baseImg, Rectangle[] rectangles)
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


        class ParseResult
        {
            public string[] ImageFiles { get; set; }
            public int ImageIndex { get; set; }
            public Rectangle[] FaceRectangles { get; set; }
            public Rectangle PlateRectangle { get; set; }
        }
    }
}
