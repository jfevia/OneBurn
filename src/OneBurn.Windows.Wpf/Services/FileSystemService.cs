using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OneBurn.FileSystem;

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
        ///     Gets the directory items asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory items.</returns>
        public async Task<IEnumerable<DirectoryItem>> GetDirectoryItemsAsync(string path)
        {
            return await Task.Run(() => GetDirectoryItems(path));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the directory items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory items.</returns>
        public IEnumerable<DirectoryItem> GetDirectoryItems(string path)
        {
            try
            {
                var directoryInfos = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).Select(s => new DirectoryInfo(s));
                var directoryItems = new List<DirectoryItem>();
                foreach (var directoryInfo in directoryInfos)
                {
                    var directoryItem = new DirectoryItem
                    {
                        Name = directoryInfo.Name,
                        Path = directoryInfo.FullName
                    };
                    directoryItems.Add(directoryItem);
                }

                return directoryItems;
            }
            catch
            {
                return Enumerable.Empty<DirectoryItem>();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the file items asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file items.</returns>
        public async Task<IEnumerable<FileItem>> GetFileItemsAsync(string path)
        {
            return await Task.Run(() => GetFileItems(path));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the file items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file items.</returns>
        public IEnumerable<FileItem> GetFileItems(string path)
        {
            try
            {
                var fileInfos = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Select(s => new FileInfo(s));
                var fileItems = new List<FileItem>();
                foreach (var fileInfo in fileInfos)
                {
                    var directoryItem = new FileItem
                    {
                        Name = fileInfo.Name,
                        Path = fileInfo.FullName
                    };
                    fileItems.Add(directoryItem);
                }

                return fileItems;
            }
            catch
            {
                return Enumerable.Empty<FileItem>();
            }
        }
    }
}