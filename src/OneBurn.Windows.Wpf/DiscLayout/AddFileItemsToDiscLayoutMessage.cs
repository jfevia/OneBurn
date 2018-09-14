using System.Collections.Generic;
using OneBurn.Windows.Shell.FileSystem;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal class AddFileItemsToDiscLayoutMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddFileItemsToDiscLayoutMessage" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public AddFileItemsToDiscLayoutMessage(IEnumerable<FileItemViewModelBase> items)
        {
            Items = items;
        }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public IEnumerable<FileItemViewModelBase> Items { get; }
    }
}