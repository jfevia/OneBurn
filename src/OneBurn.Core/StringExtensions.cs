using System;
using System.Text.RegularExpressions;

namespace OneBurn.Core
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Expands the special folders contained in a string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The string with the expanded special folders.</returns>
        public static string ExpandSpecialFolders(this string str)
        {
            var validString = str;
            foreach (Match match in Regex.Matches(validString, @"%([a-zA-Z]+)%"))
            {
                var capture = match.Groups[1].Value;
                var specialFolder = SpecialFolderHelpers.Parse(capture);
                validString = validString.Replace($"%{capture}%", Environment.GetFolderPath(specialFolder));
            }

            return validString;
        }
    }
}