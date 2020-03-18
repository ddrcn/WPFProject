using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;


namespace MyKDZ.Model
{
    class WallpaperConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Image _photo = (Image)values[0];
            MediaElement _video = (MediaElement)values[1];
            if ((bool)values[2])
            {
                _video.Play();
                return values[1];
            }
            _video.Pause();
            return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
