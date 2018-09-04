using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IFileSystemService
    {
        /// <summary>
        ///     Gets the directories asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directories.</returns>
        Task<IEnumerable<DirectoryInfo>> GetDirectoriesAsync(string path);

        /// <summary>
        ///     Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directories.</returns>
        IEnumerable<DirectoryInfo> GetDirectories(string path);

        /// <summary>
        ///     Gets the files asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The files.</returns>
        Task<IEnumerable<FileInfo>> GetFilesAsync(string path);

        /// <summary>
        ///     Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The files.</returns>
        IEnumerable<FileInfo> GetFiles(string path);
    }
}