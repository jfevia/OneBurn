using System.Windows;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Services
{
    internal class RadMessageBoxService : IMessageBoxService
    {
        /// <summary>
        ///     Initializes the <see cref="RadMessageBoxService" /> class.
        /// </summary>
        static RadMessageBoxService()
        {
            if (Instance == null)
                Instance = new RadMessageBoxService();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static RadMessageBoxService Instance { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Shows the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="content">The content.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="image">The image.</param>
        /// <returns>The result.</returns>
        public bool? Show(string header, string content, MessageBoxButton buttons, MessageBoxImage image)
        {
            bool? result = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var parameters = new DialogParameters();
                if (Application.Current.MainWindow?.IsLoaded == true)
                {
                    parameters.Owner = Application.Current.MainWindow;
                    parameters.DialogStartupLocation = WindowStartupLocation.CenterOwner;
                    parameters.Content = content;
                    parameters.Header = header;
                    parameters.Closed = (o, e) => OnClose(e, out result);

                    var styleName = $"RadMessageBox{image}{buttons}Style";
                    parameters.ContentStyle = Application.Current.Resources[styleName] as Style;

                    RadWindow.Prompt(parameters);
                }
                else
                {
                    MessageBox.Show(content, header, buttons, image);
                }
            });
            return result;
        }

        /// <summary>
        ///     Raises the Close event.
        /// </summary>
        /// <param name="e">The <see cref="WindowClosedEventArgs" /> instance containing the event data.</param>
        /// <param name="result">The result.</param>
        private static void OnClose(WindowClosedEventArgs e, out bool? result)
        {
            result = e.DialogResult;
        }
    }
}