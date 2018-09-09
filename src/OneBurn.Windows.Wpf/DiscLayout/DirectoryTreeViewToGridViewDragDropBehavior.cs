using System.Collections;
using OneBurn.Windows.Controls;
using OneBurn.Windows.Shell.FileSystem;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal sealed class DirectoryTreeViewToGridViewDragDropBehavior : RadDragDropBehavior<DirectoryTreeViewToGridViewDragDropBehavior, RadTreeView, RadGridView, DirectoryItemViewModelBase>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="T:Telerik.Windows.DragDrop.DragInitializeEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        /// <returns>
        ///     The container.
        /// </returns>
        protected override IDragDropContainer<RadTreeView, RadGridView, DirectoryItemViewModelBase> GetContainer(object sender, DragInitializeEventArgs e)
        {
            if (!(e.OriginalSource is RadTreeViewItem source))
                return null;

            return new ShellDragDropContainer
            {
                Item = source.Item as DirectoryItemViewModelBase
            };
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the items source.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>
        ///     The items source.
        /// </returns>
        protected override IList GetItemsSource(RadGridView destination)
        {
            return destination?.ItemsSource as IList;
        }
    }
}