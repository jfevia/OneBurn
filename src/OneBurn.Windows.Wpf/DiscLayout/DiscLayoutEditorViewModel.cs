using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Commands;
using OneBurn.Windows.Shell.Containers;
using OneBurn.Windows.Shell.DiscLayout;
using OneBurn.Windows.Shell.FileSystem;
using OneBurn.Windows.Wpf.Containers;
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

            PrimaryViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new SingleContainerViewModel
                {
                    Title = "File",
                    ViewModels = new ObservableCollection<ContextViewModelBase>
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
        ///     Converts the item its equivalent view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The view model.</returns>
        private static FileItemViewModel ToViewModel(FileInfo item)
        {
            return new FileItemViewModel
            {
                Name = item.Name,
                Path = item.FullName,
                Size = item.Length
            };
        }
    }
}