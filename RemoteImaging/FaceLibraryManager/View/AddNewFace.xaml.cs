using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Damany.Util;
using FaceSearchWrapper;
using OpenCvSharp;
using Damany.Imaging.Extensions;

namespace FaceLibraryManager.View
{
    /// <summary>
    /// Interaction logic for AddNewFace.xaml
    /// </summary>
    public partial class AddNewFace : Window
    {
        private static FaceSearchWrapper.FaceSearch searcher 
            = new FaceSearch();



        public AddNewFace()
        {
            InitializeComponent();
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            bool isFile = e.Data.GetDataPresent(DataFormats.FileDrop);
            if (isFile)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                string extenstion = System.IO.Path.GetExtension(files[0]);
                if (string.Compare(extenstion, ".jpg", false) == 0)
                {
                    var vm = (ViewModel.AddSuspectViewModel) this.DataContext;
                    vm.CurrentSuspect.ImageFilePath = files[0];

                    using (var ipl = IplImage.FromFile(files[0]))
                    {
                        var facesRects = ipl.LocateFaces(searcher);

                        if (facesRects.Length > 0)
                        {
                            vm.CurrentFaceRect = facesRects[0];

                            ipl.DrawRect(facesRects[0], CvColor.Black, 2);

                            var ms = new MemoryStream();
                            var bitmap = ipl.ToBitmap();


                            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                bitmap.GetHbitmap(),
                                IntPtr.Zero,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());
                            this.suspectInfo1.image1.Source = bitmapSource;
                        }

                    }

                }
            }


        }

    
    }
}
