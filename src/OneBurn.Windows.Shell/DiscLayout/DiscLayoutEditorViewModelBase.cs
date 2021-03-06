﻿using System.Collections.ObjectModel;
using OneBurn.Windows.Shell.Containers;
using OneBurn.Windows.Shell.FileSystem;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class DiscLayoutEditorViewModelBase : CompositeContainerViewModel
    {
        private ObservableCollection<DirectoryItemViewModelBase> _directoryItems;
        private ObservableCollection<LayoutRootViewModelBase> _layoutRoot;
        private string _projectFilePath;
        private DirectoryItemViewModelBase _selectedDirectoryItem;
        private FileItemViewModelBase _selectedFileItem;
        private LayoutFolderViewModelBase _selectedLayoutFolder;

        /// <summary>
        ///     Gets or sets the selected directory item.
        /// </summary>
        /// <value>
        ///     The selected directory item.
        /// </value>
        public virtual DirectoryItemViewModelBase SelectedDirectoryItem
        {
            get => _selectedDirectoryItem;
            set => Set(ref _selectedDirectoryItem, value);
        }

        /// <summary>
        ///     Gets or sets the project file path.
        /// </summary>
        /// <value>
        ///     The project file path.
        /// </value>
        public string ProjectFilePath
        {
            get => _projectFilePath;
            set => Set(ref _projectFilePath, value);
        }

        /// <summary>
        ///     Gets or sets the directory items.
        /// </summary>
        /// <value>
        ///     The directory items.
        /// </value>
        public ObservableCollection<DirectoryItemViewModelBase> DirectoryItems
        {
            get => _directoryItems;
            set => Set(ref _directoryItems, value);
        }

        /// <summary>
        ///     Gets or sets the layout root.
        /// </summary>
        /// <value>
        ///     The layout root.
        /// </value>
        public ObservableCollection<LayoutRootViewModelBase> LayoutRoot
        {
            get => _layoutRoot;
            set => Set(ref _layoutRoot, value);
        }

        /// <summary>
        ///     Gets or sets the selected file item.
        /// </summary>
        /// <value>
        ///     The selected file item.
        /// </value>
        public virtual FileItemViewModelBase SelectedFileItem
        {
            get => _selectedFileItem;
            set => Set(ref _selectedFileItem, value);
        }

        /// <summary>
        ///     Gets or sets the selected layout folder.
        /// </summary>
        /// <value>
        ///     The selected layout folder.
        /// </value>
        public virtual LayoutFolderViewModelBase SelectedLayoutFolder
        {
            get => _selectedLayoutFolder;
            set => Set(ref _selectedLayoutFolder, value);
        }
    }
}