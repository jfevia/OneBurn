namespace OneBurn.FileSystem
{
    public class FileItem : FileSystemItem, IFileItem
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public virtual long Size { get; set; }
    }
}