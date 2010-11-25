using System;
using System.Globalization;
using System.Windows.Data;
using Damany.Util;

namespace FaceLibraryManager
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Damany.Util.Gender g = (Gender)value;

            switch (g)
            {
                case Gender.Male:
                    return "��";
                    break;
                case Gender.Female:
                    return "Ů";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}