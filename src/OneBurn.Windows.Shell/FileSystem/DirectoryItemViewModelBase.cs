using System.Collections.ObjectModel;
using OneBurn.FileSystem;

namespace OneBurn.Windows.Shell.FileSystem
{
    public abstract class DirectoryItemViewModelBase : FileSystemItemViewModelBase, IDirectoryItem
    {
        private ObservableCollection<DirectoryItemViewModelBase> _childDirectories;
        private ObservableCollection<FileItemViewModelBase> _childFiles;

        /// <summary>
        ///     Gets the child directories.
        /// </summary>
        /// <value>
        ///     The child directories.
        /// </value>
        public virtual ObservableCollection<DirectoryItemViewModelBase> ChildDirectories
        {
            get => _childDirectories;
            set => Set(ref _childDirectories, value);
        }

        /// <summary>
        ///     Gets or sets the child files.
        /// </summary>
        /// <value>
        ///     The child files.
        /// </value>
        public virtual ObservableCollection<FileItemViewModelBase> ChildFiles
        {
            get => _childFiles;
            set => Set(ref _childFiles, value);
        }
    }
}