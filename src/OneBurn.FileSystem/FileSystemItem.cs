namespace OneBurn.FileSystem
{
    public abstract class FileSystemItem : IFileSystemItem
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        public virtual string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public virtual string Path { get; set; }
    }
}