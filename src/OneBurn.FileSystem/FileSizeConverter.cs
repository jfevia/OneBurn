using System;

namespace OneBurn.FileSystem
{
    public static class FileSizeConverter
    {
        /// <summary>
        ///     Converts the specified size to its equivalent string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>The equivalent string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">unit - null</exception>
        public static string ConvertToString(long size, SizeUnit unit)
        {
            string unitStr;
            var unitPower = 0;

            if (unit == SizeUnit.Auto)
            {
                var refSize = size;
                while (refSize > 1024)
                {
                    unitPower++;
                    refSize /= 1024;
                }

                unitStr = GetUnitStringFromPower(unitPower, refSize);
            }
            else
            {
                unitPower = GetPowerFromUnit(unit);
                unitStr = GetUnitStringFromPower(unitPower, size);
            }

            var value = Math.Round(size / Math.Pow(1024, unitPower), 2);
            return unitPower > 0 ? $"{value:#.00}{unitStr}" : $"{value}{unitStr}";
        }

        /// <summary>
        ///     Gets the power from the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The power.</returns>
        /// <exception cref="ArgumentOutOfRangeException">unit - null</exception>
        public static int GetPowerFromUnit(SizeUnit unit)
        {
            switch (unit)
            {
                case SizeUnit.Byte:
                    return 0;
                case SizeUnit.Kilobyte:
                    return 1;
                case SizeUnit.Megabyte:
                    return 2;
                case SizeUnit.Gigabyte:
                    return 3;
                case SizeUnit.Terabyte:
                    return 4;
                case SizeUnit.Petabyte:
                    return 5;
                case SizeUnit.Exabyte:
                    return 6;
                case SizeUnit.Zettabyte:
                    return 7;
                case SizeUnit.Yottabyte:
                    return 8;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
            }
        }

        /// <summary>
        ///     Gets the unit string from the specified power.
        /// </summary>
        /// <param name="power">The power.</param>
        /// <param name="size">The size.</param>
        /// <returns>The unit string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">power - null</exception>
        public static string GetUnitStringFromPower(int power, long size)
        {
            switch (power)
            {
                case 0:
                    return $" byte{(size > 1 ? "s" : string.Empty)}";
                case 1:
                    return "KB";
                case 2:
                    return "MB";
                case 3:
                    return "GB";
                case 4:
                    return "TB";
                case 5:
                    return "PB";
                case 6:
                    return "EB";
                case 7:
                    return "ZB";
                case 8:
                    return "YB";
                default:
                    throw new ArgumentOutOfRangeException(nameof(power), power, null);
            }
        }
    }
}