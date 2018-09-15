using System.Collections.ObjectModel;
using OneBurn.DiscLayout;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class LayoutFolderViewModelBase : LayoutNodeViewModelBase, ILayoutFolder
    {
        private ObservableCollection<LayoutFileViewModelBase> _files;
        private ObservableCollection<LayoutFolderViewModelBase> _folders;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LayoutFolderViewModelBase" /> class.
        /// </summary>
        protected LayoutFolderViewModelBase()
        {
            _folders = new ObservableCollection<LayoutFolderViewModelBase>();
            _files = new ObservableCollection<LayoutFileViewModelBase>();
        }

        /// <summary>
        ///     Gets or sets the folders.
        /// </summary>
        /// <value>
        ///     The folders.
        /// </value>
        public virtual ObservableCollection<LayoutFolderViewModelBase> Folders
        {
            get => _folders;
            set => Set(ref _folders, value);
        }

        /// <summary>
        ///     Gets or sets the child files.
        /// </summary>
        /// <value>
        ///     The child files.
        /// </value>
        public virtual ObservableCollection<LayoutFileViewModelBase> Files
        {
            get => _files;
            set => Set(ref _files, value);
        }
    }
}