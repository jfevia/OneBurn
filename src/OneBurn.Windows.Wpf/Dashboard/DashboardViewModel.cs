using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Dashboard;
using OneBurn.Windows.Wpf.Burning;
using OneBurn.Windows.Wpf.Properties;

namespace OneBurn.Windows.Wpf.Dashboard
{
    internal sealed class DashboardViewModel : DashboardViewModelBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DashboardViewModel" /> class.
        /// </summary>
        public DashboardViewModel()
        {
            Title = "Dashboard";
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
                new BurnOptionsViewModel()
            };

            await OnSetCurrentViewModel(ViewModels.FirstOrDefault());
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