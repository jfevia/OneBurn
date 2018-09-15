using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using OneBurn.FileSystem;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Commands;
using OneBurn.Windows.Shell.Containers;
using OneBurn.Windows.Shell.DiscLayout;
using OneBurn.Windows.Shell.FileSystem;
using OneBurn.Windows.Shell.Services;
using OneBurn.Windows.Shell.WindowsApi;
using OneBurn.Windows.Wpf.Containers;
using OneBurn.Windows.Wpf.DiscLayout.Serialization;
using OneBurn.Windows.Wpf.FileSystem;
using OneBurn.Windows.Wpf.Services;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal sealed class DiscLayoutEditorViewModel : DiscLayoutEditorViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiscLayoutEditorViewModel" /> class.
        /// </summary>
        public DiscLayoutEditorViewModel()
        {
            Title = "Disc Layout Editor";
            Description = "Edit and configure the layout of the disk";

            LoadChildDirectoriesCommand = new AsyncCommand<object>(LoadChildDirectoriesAsync);
            LoadChildFilesCommand = new AsyncCommand(LoadChildFilesAsync);

            MessagingService.Instance.Register<AddDirectoryItemToDiscLayoutMessage>(this, OnAddDirectoryItemToDiscLayout);
            MessagingService.Instance.Register<AddFileItemsToDiscLayoutMessage>(this, OnAddFileItemsToDiscLayout);
        }

        /// <summary>
        ///     Gets the load child directories command.
        /// </summary>
        /// <value>
        ///     The load child directories command.
        /// </value>
        public ICommand LoadChildDirectoriesCommand { get; }

        /// <summary>
        ///     Gets the load child files command.
        /// </summary>
        /// <value>
        ///     The load child files command.
        /// </value>
        public ICommand LoadChildFilesCommand { get; }

        /// <summary>
        ///     Called when [add file items to disc layout].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnAddFileItemsToDiscLayout(AddFileItemsToDiscLayoutMessage message)
        {
            if (SelectedLayoutFolder == null)
                return;

            foreach (var item in message.Items.Select(ToLayoutFile))
                SelectedLayoutFolder.Files.Add(item);
        }

        /// <summary>
        ///     Called when [add directory item to disc layout].
        /// </summary>
        /// <param name="message">The message.</param>
        private async void OnAddDirectoryItemToDiscLayout(AddDirectoryItemToDiscLayoutMessage message)
        {
            IsBusy = true;
            await PopulateLayoutNode(LayoutRoot.First(), message.DirectoryItem);
            IsBusy = false;
        }

        /// <summary>
        ///     Populates the layout node.
        /// </summary>
        /// <param name="layoutNode">The layout node.</param>
        /// <param name="directoryItem">The directory item.</param>
        /// <returns>The task.</returns>
        private static async Task PopulateLayoutNode(LayoutFolderViewModelBase layoutNode, DirectoryItemViewModel directoryItem)
        {
            var item = new LayoutFolderViewModel();
            item.Name = directoryItem.Name;
            item.Path = directoryItem.Path;
            layoutNode.Folders.Add(item);

            var files = await FileSystemService.Instance.GetFilesAsync(item.Path);
            item.Files = new ObservableCollection<LayoutFileViewModelBase>(files.Select(ToLayoutFile));

            var childDirectories = (await FileSystemService.Instance.GetDirectoriesAsync(item.Path)).ToList();
            foreach (var childDirectory in childDirectories)
                await PopulateLayoutNode(item, ToViewModel(childDirectory));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            MessagingService.Instance.Unregister<AddDirectoryItemToDiscLayoutMessage>(this);

            base.Dispose();
        }

        /// <summary>
        ///     Loads the child files asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task LoadChildFilesAsync()
        {
            if (SelectedDirectoryItem == null)
                return;

            SelectedDirectoryItem.ChildFiles = new ObservableCollection<FileItemViewModelBase>(await GetFileItems(SelectedDirectoryItem.Path));
        }

        /// <summary>
        ///     Loads the directory item children asynchronously.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The task.</returns>
        private static async Task<object> LoadChildDirectoriesAsync(object obj)
        {
            if (!(obj is RoutedEventArgs eventArgs))
                return null;

            if (!(eventArgs.OriginalSource is RadTreeViewItem radTreeViewItem))
                return null;

            if (!(radTreeViewItem.DataContext is DirectoryItemViewModel directoryItemViewModel))
                return null;

            directoryItemViewModel.ChildDirectories = new ObservableCollection<DirectoryItemViewModelBase>(await GetDirectoryItems(directoryItemViewModel.Path));
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Loads the data asynchronously.
        /// </summary>
        /// <returns>
        ///     The task.
        /// </returns>
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            LayoutRoot = new ObservableCollection<LayoutRootViewModelBase>
            {
                new LayoutRootViewModel
                {
                    Name = "Root"
                }
            };
            SelectedLayoutFolder = LayoutRoot.FirstOrDefault();

            PrimaryViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new SingleContainerViewModel
                {
                    Title = "File",
                    ViewModels = new ObservableCollection<ContextViewModelBase>
                    {
                        new CommandContainerViewModel
                        {
                            Title = "Open...",
                            Command = new AsyncCommand(OpenProjectAsync)
                        },
                        new CommandContainerViewModel
                        {
                            Title = "Close"
                        },
                        new CommandContainerViewModel
                        {
                            Title = "Save",
                            Command = new AsyncCommand(SaveProjectAsync)
                        },
                        new CommandContainerViewModel
                        {
                            Title = "Save As...",
                            Command = new AsyncCommand(CloneProjectAsync)
                        },
                        new CommandContainerViewModel
                        {
                            Title = "Options..."
                        },
                        new CommandContainerViewModel
                        {
                            Title = "Exit"
                        }
                    }
                }
            };

            SecondaryViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new CommandContainerViewModel
                {
                    Title = "Open..."
                },
                new CommandContainerViewModel
                {
                    Title = "Close"
                },
                new CommandContainerViewModel
                {
                    Title = "Save"
                },
                new CommandContainerViewModel
                {
                    Title = "Save As..."
                },
                new CommandContainerViewModel
                {
                    Title = "Options..."
                },
                new CommandContainerViewModel
                {
                    Title = "Exit"
                }
            };

            await LoadDirectoryItemsAsync();
        }

        /// <summary>
        ///     Opens a project asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task OpenProjectAsync()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.Title = "Open...";
            openFileDialog.Filter = "OneBurn Project (*.obp)|*.obp";

            var result = openFileDialog.ShowDialog();
            if (result != true)
                return;

            await LoadProjectAsync(openFileDialog.FileName);
        }

        /// <summary>
        ///     Loads the project asynchronously.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        /// <exception cref="System.ArgumentNullException">fileName</exception>
        private async Task LoadProjectAsync(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            LayoutRoot = new ObservableCollection<LayoutRootViewModelBase>
            {
                await DiscLayoutSerializationService.Instance.DeserializeAsync(File.ReadAllText(fileName))
            };
        }

        /// <summary>
        ///     Saves the project to a new file path asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task CloneProjectAsync()
        {
            var outputFilePath = GetOutputFilePath();
            if (string.IsNullOrWhiteSpace(outputFilePath))
                return;

            await SaveProjectAsync(outputFilePath, LayoutRoot.First());
        }

        /// <summary>
        ///     Saves the project asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task SaveProjectAsync()
        {
            var outputFilePath = GetOutputFilePath(ProjectFilePath);
            if (string.IsNullOrWhiteSpace(outputFilePath))
                return;

            await SaveProjectAsync(outputFilePath, LayoutRoot.First());
        }

        /// <summary>
        ///     Saves the project asynchronously.
        /// </summary>
        /// <param name="projectFilePath">The project file path.</param>
        /// <param name="layoutRoot">The layout root.</param>
        /// <returns>The task.</returns>
        private static async Task SaveProjectAsync(string projectFilePath, LayoutRootViewModelBase layoutRoot)
        {
            if (projectFilePath == null) throw new ArgumentNullException(nameof(projectFilePath));
            if (layoutRoot == null) throw new ArgumentNullException(nameof(layoutRoot));

            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, await DiscLayoutSerializationService.Instance.SerializeAsync(layoutRoot));

            using (var originalFileStream = File.OpenRead(tempFilePath))
            using (var compressedFileStream = File.Create(projectFilePath))
            using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                originalFileStream.CopyTo(compressionStream);

            File.Delete(tempFilePath);
        }

        /// <summary>
        ///     Gets the output file path.
        /// </summary>
        /// <param name="defaultFilePath">The default file path.</param>
        /// <returns>The output file path.</returns>
        private static string GetOutputFilePath(string defaultFilePath = null)
        {
            if (!string.IsNullOrWhiteSpace(defaultFilePath))
                return defaultFilePath;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.Title = "Save As...";
            saveFileDialog.Filter = "OneBurn Project (*.obp)|*.obp";

            var result = saveFileDialog.ShowDialog();
            return result != true ? null : saveFileDialog.FileName;
        }

        /// <summary>
        ///     Loads the directory items asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task LoadDirectoryItemsAsync()
        {
            DirectoryItems = new ObservableCollection<DirectoryItemViewModelBase>(await GetDirectoryItems(@"C:\"));
        }

        /// <summary>
        ///     Gets the directory items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory items.</returns>
        private static async Task<IEnumerable<DirectoryItemViewModel>> GetDirectoryItems(string path)
        {
            var directoryItems = await FileSystemService.Instance.GetDirectoriesAsync(path);
            return directoryItems.Select(ToViewModel);
        }

        /// <summary>
        ///     Gets the file items.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file items.</returns>
        private static async Task<IEnumerable<FileItemViewModel>> GetFileItems(string path)
        {
            var fileItems = await FileSystemService.Instance.GetFilesAsync(path);
            return fileItems.Select(ToViewModel);
        }

        /// <summary>
        ///     Converts the item its equivalent view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The view model.</returns>
        private static DirectoryItemViewModel ToViewModel(DirectoryInfo item)
        {
            return new DirectoryItemViewModel
            {
                Name = item.Name,
                Path = item.FullName,
                ChildDirectories = new ObservableCollection<DirectoryItemViewModelBase>
                {
                    new DirectoryItemViewModel
                    {
                        Title = "Loading..."
                    }
                }
            };
        }

        /// <summary>
        ///     Converts the item to its equivalent view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The view model.</returns>
        private static FileItemViewModel ToViewModel(FileInfo item)
        {
            return new FileItemViewModel
            {
                Name = item.Name,
                Path = item.FullName,
                Size = item.Length,
                DateModified = item.LastWriteTime,
                TypeName = Windows.Shell.WindowsApi.FileSystem.GetTypeFriendlyName(item.FullName),
                Icon = IconManager.FindIconForFilename(item.FullName, false)
            };
        }

        /// <summary>
        ///     Converts the item to its equivalent layout file.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The view model</returns>
        private static LayoutFileViewModel ToLayoutFile(IFileItem item)
        {
            return new LayoutFileViewModel
            {
                Name = item.Name,
                Path = item.Path,
                Size = item.Size,
                DateModified = item.DateModified,
                TypeName = Windows.Shell.WindowsApi.FileSystem.GetTypeFriendlyName(item.Path),
                Icon = IconManager.FindIconForFilename(item.Path, false)
            };
        }

        /// <summary>
        ///     Converts the item to its equivalent layout file.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The view model</returns>
        private static LayoutFileViewModel ToLayoutFile(FileInfo item)
        {
            return new LayoutFileViewModel
            {
                Name = item.Name,
                Path = item.FullName,
                Size = item.Length,
                DateModified = item.LastWriteTime,
                TypeName = Windows.Shell.WindowsApi.FileSystem.GetTypeFriendlyName(item.FullName),
                Icon = IconManager.FindIconForFilename(item.FullName, false)
            };
        }
    }
}