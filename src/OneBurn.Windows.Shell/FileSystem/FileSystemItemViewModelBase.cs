﻿using OneBurn.FileSystem;

namespace OneBurn.Windows.Shell.FileSystem
{
    public class FileSystemItemViewModelBase : ContextViewModelBase, IFileSystemItem
    {
        private string _name;
        private string _path;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public string Path
        {
            get => _path;
            set => Set(ref _path, value);
        }
    }
}