using System.Collections.Generic;
using System.Threading.Tasks;
using OneBurn.FileSystem;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IFileSystemService
    {
        /// <summary>
        ///     Gets the directory items asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory items.</returns>
        Task<IEnumerable<DirectoryItem>> GetDirectoryItemsAsync(string path);

        /// <summary>
        ///     Gets the directory items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory items.</returns>
        IEnumerable<DirectoryItem> GetDirectoryItems(string path);

        /// <summary>
        ///     Gets the file items asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file items.</returns>
        Task<IEnumerable<FileItem>> GetFileItemsAsync(string path);

        /// <summary>
        ///     Gets the file items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file items.</returns>
        IEnumerable<FileItem> GetFileItems(string path);
    }
}