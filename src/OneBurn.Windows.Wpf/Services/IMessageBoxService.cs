using System.Windows;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IMessageBoxService
    {
        /// <summary>
        ///     Shows the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="content">The content.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="image">The image.</param>
        /// <returns>The result.</returns>
        bool? Show(string header, string content, MessageBoxButton buttons, MessageBoxImage image);
    }
}