using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OneBurn.Windows.Shell;
using OneBurn.Windows.Shell.Configuration;

namespace OneBurn.Windows.Wpf.Configuration
{
    internal sealed class SettingsViewModel : SettingsViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsViewModel" /> class.
        /// </summary>
        public SettingsViewModel()
        {
            Title = "Settings";

            SetCurrentViewModelCommand = new AsyncCommand<object>(OnSetCurrentViewModel);
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

            ViewModels = new ObservableCollection<ContextViewModelBase>
            {
                new GeneralSettingsViewModel()
            };

            await OnSetCurrentViewModel(ViewModels.FirstOrDefault());
        }

        /// <summary>
        ///     Called when [set current view model].
        /// </summary>
        /// <param name="obj">The object.</param>
        private async Task OnSetCurrentViewModel(object obj)
        {
            CurrentViewModel = (ContextViewModelBase) obj;
            await CurrentViewModel.InitializeAsync();
        }
    }
}