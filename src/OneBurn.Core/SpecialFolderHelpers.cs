using System;

namespace OneBurn.Core
{
    public static class SpecialFolderHelpers
    {
        /// <summary>
        ///     Parses the specified value into a special folder.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The special folder.</returns>
        public static Environment.SpecialFolder Parse(string value)
        {
            return (Environment.SpecialFolder) Enum.Parse(typeof(Environment.SpecialFolder), value, true);
        }
    }
}