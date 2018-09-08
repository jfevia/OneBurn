using System;
using System.Windows.Media;
using OneBurn.Windows.Shell.FileSystem;

namespace OneBurn.Windows.Wpf.FileSystem
{
    internal sealed class FileItemViewModel : FileItemViewModelBase
    {
        /// <summary>
        ///     Gets or sets the date modified.
        /// </summary>
        /// <value>
        ///     The date modified.
        /// </value>
        public DateTime DateModified { get; set; }

        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>
        ///     The name of the type.
        /// </value>
        public string TypeName { get; set; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public ImageSource Icon { get; set; }
    }
}