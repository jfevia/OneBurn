using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OneBurn.Windows.Controls;
using OneBurn.Windows.Shell.FileSystem;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal sealed class FileGridViewToGridViewDragDropBehavior : RadDragDropBehavior<FileGridViewToGridViewDragDropBehavior, RadGridView, RadGridView, IEnumerable<FileItemViewModelBase>>
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
        protected override IDragDropContainer<RadGridView, RadGridView, IEnumerable<FileItemViewModelBase>> GetContainer(object sender, DragInitializeEventArgs e)
        {
            if (!(sender is RadGridView destination))
                return null;

            return new FileListDragDropContainer
            {
                Item = destination.SelectedItems.Cast<FileItemViewModelBase>()
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

        /// <inheritdoc />
        /// <summary>
        ///     Called when [drop].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="T:OneBurn.Windows.Wpf.DiscLayout.DragDropCompletedEventArgs`2" /> instance containing the event
        ///     data.
        /// </param>
        protected override void OnDrop(object sender, DragDropCompletedEventArgs<RadGridView, RadGridView, IEnumerable<FileItemViewModelBase>> e)
        {
            if (e.DragDropEffects == DragDropEffects.None)
                return;

            foreach (var item in e.Container.Item)
                e.ItemsSource.Add(item);
        }
    }
}