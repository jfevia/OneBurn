using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OneBurn.Core;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Containers;
using OneBurn.Windows.Wpf.About;
using OneBurn.Windows.Wpf.Configuration;
using OneBurn.Windows.Wpf.Dashboard;
using OneBurn.Windows.Wpf.Properties;

namespace OneBurn.Windows.Wpf.Shell
{
    internal sealed class MainWindowViewModel : CompositeContainerViewModelBase
    {
        private Assembly _assembly;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Windows.Shell.MainWindowViewModel" /> class.
        /// </summary>
        public MainWindowViewModel()
        {
            SetCurrentViewModelCommand = new AsyncCommand<object>(OnSetCurrentViewModel);
        }

        /// <summary>
        ///     Gets the assembly version.
        /// </summary>
        /// <value>
        ///     The assembly version.
        /// </value>
        public Version AssemblyVersion => (_assembly ?? (_assembly = Assembly.GetExecutingAssembly())).GetVersion();

        /// <inheritdoc />
        /// <summary>
        ///     Loads the data asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            PrimaryViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new DashboardViewModel()
            };
            SecondaryViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new SettingsViewModel(),
                new AboutViewModel()
            };

            await OnSetCurrentViewModel(PrimaryViewModels.FirstOrDefault());
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