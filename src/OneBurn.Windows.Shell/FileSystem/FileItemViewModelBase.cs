using OneBurn.FileSystem;

namespace OneBurn.Windows.Shell.FileSystem
{
    public abstract class FileItemViewModelBase : FileSystemItemViewModelBase, IFileItem
    {
        private long _size;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public virtual long Size
        {
            get => _size;
            set => Set(ref _size, value);
        }
    }
}