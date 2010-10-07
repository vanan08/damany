using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FaceLibraryManager
{
    public sealed class ImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            try
            {
                var fileName = value as string;
                var data = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(fileName));
                var bitmap = BitmapFrame.Create(data);

                return bitmap;
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
