using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyKDZ.Model
{
    class HeroesValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "int")
            {
                try
                {
                    int _val = int.Parse(value.ToString());
                    if (_val<0)
                    {
                        return 0;
                    }
                    return _val;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            if (parameter.ToString() == "double")
            {
                try
                {
                    double _val = double.Parse(value.ToString());
                    if (_val < 0)
                    {
                        return 0.0;
                    }
                    return _val;
                }
                catch (Exception)
                {
                    return 0.0;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString()=="int")
            {
                try
                {
                    int _val = int.Parse(value.ToString()) ;
                    if (_val < 0)
                    {
                        return 0;
                    }
                    return _val;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            if (parameter.ToString()=="double")
            {
                try
                {
                    double _val = double.Parse(value.ToString(),CultureInfo.InvariantCulture);
                    if (_val < 0)
                    {
                        return 0.0;
                    }
                    return _val;
                }
                catch (Exception)
                {
                    return 0.0;
                }
            }
            return null;
        }
    }
}
