﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Team_Manager
{
    public class PercentConverter : IValueConverter
    {
        /// <summary>
        /// Converts a percent to a combobox index
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Percent))
                return 0;

            Percent Completion = (value as Nullable<Percent>) ?? default;

            return Completion.Value switch
            {
                100 => 6,
                90 => 5,
                75 => 4,
                50 => 3,
                25 => 2,
                10 => 1,
                _ => 0,
            };
        }

        /// <summary>
        /// Converts a combobox index to a percent
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = System.Convert.ToInt32(value);

            return index switch
            {
                6 => new Percent(100),
                5 => new Percent(90),
                4 => new Percent(75),
                3 => new Percent(50),
                2 => new Percent(25),
                1 => new Percent(10),
                _ => new Percent(0),
            };
        }
    }
}