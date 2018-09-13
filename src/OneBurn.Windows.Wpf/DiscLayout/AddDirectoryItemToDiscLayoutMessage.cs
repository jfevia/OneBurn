using OneBurn.Windows.Shell.Messaging;
using OneBurn.Windows.Wpf.FileSystem;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal class AddDirectoryItemToDiscLayoutMessage : MessageBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddDirectoryItemToDiscLayoutMessage" /> class.
        /// </summary>
        /// <param name="directoryItem">The directory item.</param>
        public AddDirectoryItemToDiscLayoutMessage(DirectoryItemViewModel directoryItem)
        {
            DirectoryItem = directoryItem;
        }

        /// <summary>
        ///     Gets the directory item.
        /// </summary>
        /// <value>
        ///     The directory item.
        /// </value>
        public DirectoryItemViewModel DirectoryItem { get; }
    }
}