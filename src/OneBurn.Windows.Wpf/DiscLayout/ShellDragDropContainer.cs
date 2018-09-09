using OneBurn.Windows.Controls;
using OneBurn.Windows.Shell.FileSystem;
using Telerik.Windows.Controls;
using ViewModelBase = OneBurn.Windows.Shell.ViewModelBase;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal class ShellDragDropContainer : ViewModelBase, IDragDropContainer<RadTreeView, RadGridView, DirectoryItemViewModelBase>
    {
        private DirectoryItemViewModelBase _item;
        private RadTreeView _source;
        private RadGridView _target;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public DirectoryItemViewModelBase Item
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
        public RadTreeView Source
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
        public RadGridView Destination
        {
            get => _target;
            set => Set(ref _target, value);
        }
    }
}