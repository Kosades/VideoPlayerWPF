using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace VideoBrowser
{
    class FileToURIConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            // In design mode, value is not a string, so it is 
            // important to check input parameters.
            if (value is string)
            {
                // Convert from the image name to a BitmapFrame
                // for display in the list.
                return BitmapFrame.Create(new Uri(value.ToString()));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}