using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Burning;
using OneBurn.Windows.Shell.Commands;
using OneBurn.Windows.Wpf.DiscLayout;
using OneBurn.Windows.Wpf.Properties;

namespace OneBurn.Windows.Wpf.Burning
{
    internal sealed class BurnOptionsViewModel : BurnOptionsViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BurnOptionsViewModel" /> class.
        /// </summary>
        public BurnOptionsViewModel()
        {
            Title = "Burn Options";

            SetCurrentViewModelCommand = new AsyncCommand<object>(OnSetCurrentViewModel);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Loads the data asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            ViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new BurnImageViewModel(),
                new BurnFilesAndFoldersViewModel(),
                new CreateImageFromMediaViewModel(),
                new CreateImageFromFilesAndFoldersViewModel(),
                new DiscLayoutEditorViewModel()
            };
        }

        /// <summary>
        ///     Called when [set current view model].
        /// </summary>
        /// <param name="obj">The object.</param>
        private async Task OnSetCurrentViewModel(object obj)
        {
            var retryCount = Settings.Default.GeneralNetworkRetryCount;
            do
            {
                try
                {
                    if (retryCount > 0)
                        retryCount--;

                    CurrentViewModel = (ContextViewModelBase) obj;
                    await CurrentViewModel.InitializeAsync();
                    break;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    await Task.Delay(Settings.Default.GeneralNetworkTimeSpanBetweenRetries);
                }
            } while (retryCount != 0);
        }
    }
}