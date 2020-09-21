using System;
using System.Globalization;
using System.Windows.Data;

namespace Team_Manager
{
    /// <summary>
    /// Converts Date Time to a string
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <summary>
        /// Convert DateTime to String
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convert = new System.ComponentModel.DateTimeConverter();
            return convert.ConvertToString(value);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}