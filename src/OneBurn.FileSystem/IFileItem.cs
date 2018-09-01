namespace OneBurn.FileSystem
{
    public interface IFileItem : IFileSystemItem
    {
        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        long Size { get; set; }
    }
}