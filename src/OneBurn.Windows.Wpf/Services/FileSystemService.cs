using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneBurn.Windows.Wpf.Services
{
    internal class FileSystemService : IFileSystemService
    {
        /// <summary>
        ///     Initializes the <see cref="FileSystemService" /> class.
        /// </summary>
        static FileSystemService()
        {
            if (Instance == null)
                Instance = new FileSystemService();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static FileSystemService Instance { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the directories asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///     The directories.
        /// </returns>
        public async Task<IEnumerable<DirectoryInfo>> GetDirectoriesAsync(string path)
        {
            return await Task.Run(() => GetDirectories(path));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///     The directories.
        /// </returns>
        public IEnumerable<DirectoryInfo> GetDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).Select(s => new DirectoryInfo(s));
            }
            catch
            {
                return Enumerable.Empty<DirectoryInfo>();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the files asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The files.</returns>
        public async Task<IEnumerable<FileInfo>> GetFilesAsync(string path)
        {
            return await Task.Run(() => GetFiles(path));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The files.</returns>
        public IEnumerable<FileInfo> GetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Select(s => new FileInfo(s));
            }
            catch
            {
                return Enumerable.Empty<FileInfo>();
            }
        }
    }
}