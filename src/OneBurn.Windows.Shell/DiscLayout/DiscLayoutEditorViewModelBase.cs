using System.Collections.ObjectModel;
using OneBurn.Windows.Shell.Containers;
using OneBurn.Windows.Shell.FileSystem;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class DiscLayoutEditorViewModelBase : CompositeContainerViewModel
    {
        private ObservableCollection<DirectoryItemViewModelBase> _directoryItems;
        private ObservableCollection<LayoutNodeViewModelBase> _layoutRoot;
        private DirectoryItemViewModelBase _selectedDirectoryItem;
        private FileItemViewModelBase _selectedFileItem;
        private LayoutNodeViewModelBase _selectedLayoutNode;

        /// <summary>
        ///     Gets or sets the selected directory item.
        /// </summary>
        /// <value>
        ///     The selected directory item.
        /// </value>
        public virtual DirectoryItemViewModelBase SelectedDirectoryItem
        {
            get => _selectedDirectoryItem;
            set => Set(ref _selectedDirectoryItem, value);
        }

        /// <summary>
        ///     Gets or sets the directory items.
        /// </summary>
        /// <value>
        ///     The directory items.
        /// </value>
        public ObservableCollection<DirectoryItemViewModelBase> DirectoryItems
        {
            get => _directoryItems;
            set => Set(ref _directoryItems, value);
        }

        /// <summary>
        ///     Gets or sets the layout root.
        /// </summary>
        /// <value>
        ///     The layout root.
        /// </value>
        public ObservableCollection<LayoutNodeViewModelBase> LayoutRoot
        {
            get => _layoutRoot;
            set => Set(ref _layoutRoot, value);
        }

        /// <summary>
        ///     Gets or sets the selected file item.
        /// </summary>
        /// <value>
        ///     The selected file item.
        /// </value>
        public virtual FileItemViewModelBase SelectedFileItem
        {
            get => _selectedFileItem;
            set => Set(ref _selectedFileItem, value);
        }

        /// <summary>
        ///     Gets or sets the selected layout node.
        /// </summary>
        /// <value>
        ///     The selected layout node.
        /// </value>
        public virtual LayoutNodeViewModelBase SelectedLayoutNode
        {
            get => _selectedLayoutNode;
            set => Set(ref _selectedLayoutNode, value);
        }
    }
}