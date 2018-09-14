using System;

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

        /// <summary>
        ///     Gets or sets the date modified.
        /// </summary>
        /// <value>
        ///     The date modified.
        /// </value>
        DateTime DateModified { get; set; }

        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>
        ///     The name of the type.
        /// </value>
        string TypeName { get; set; }
    }
}