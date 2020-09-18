using System;
using System.Globalization;
using System.Windows.Data;

namespace Team_Manager
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convert = new System.ComponentModel.DateTimeConverter();
            return convert.ConvertToString(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}