using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using RocketDivision.StarBurnX;

namespace OneBurn.Windows.Wpf.Converters
{
    public class DriveInfoToStringConverter : IValueConverter
    {
        private readonly Type _driveInfoType = typeof(IDriveInfo);

        /// <summary>
        ///     Gets or sets the format.
        /// </summary>
        /// <value>
        ///     The format.
        /// </value>
        public string Format { get; set; } = "({Letter}:) {Vendor} {Product}";

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
            if (!(value is IDriveInfo))
                return null;

            var str = Format;
            foreach (Match match in Regex.Matches(Format, @"{([a-zA-Z0-9]+)}"))
            {
                var capture = match.Groups[1].Value;
                var property = _driveInfoType.GetProperty(capture);
                if (property == null)
                    continue;

                str = str.Replace($"{{{capture}}}", System.Convert.ToString(property.GetValue(value)));
            }

            return str;
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
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}