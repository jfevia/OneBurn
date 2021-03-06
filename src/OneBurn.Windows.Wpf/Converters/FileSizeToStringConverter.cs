﻿using System;
using System.Globalization;
using System.Windows.Data;
using OneBurn.FileSystem;

namespace OneBurn.Windows.Wpf.Converters
{
    internal class FileSizeToStringConverter : IValueConverter
    {
        /// <summary>
        ///     Gets or sets the minimum unit.
        /// </summary>
        /// <value>
        ///     The minimum unit.
        /// </value>
        public SizeUnit MinimumUnit { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is long size))
                return 0;

            return FileSizeConverter.ConvertToString(size, MinimumUnit);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}