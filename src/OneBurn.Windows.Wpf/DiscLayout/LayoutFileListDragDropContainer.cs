using System.Collections.Generic;
using OneBurn.Windows.Controls;
using OneBurn.Windows.Shell.FileSystem;
using Telerik.Windows.Controls;
using ViewModelBase = OneBurn.Windows.Shell.ViewModelBase;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal class LayoutFileListDragDropContainer : ViewModelBase, IDragDropContainer<RadGridView, RadTreeView, IEnumerable<FileItemViewModelBase>>
    {
        private IEnumerable<FileItemViewModelBase> _item;
        private RadGridView _source;
        private RadTreeView _target;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public IEnumerable<FileItemViewModelBase> Item
        {
            get => _item;
            set => Set(ref _item, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        public RadGridView Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the target.
        /// </summary>
        /// <value>
        ///     The target.
        /// </value>
        public RadTreeView Destination
        {
            get => _target;
            set => Set(ref _target, value);
        }
    }
}