using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using OneBurn.Windows.Shell.Configuration;
using OneBurn.Windows.Wpf.Properties;
using OneBurn.Windows.Wpf.Services;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Configuration
{
    internal sealed class GeneralSettingsViewModel : GeneralSettingsViewModelBase
    {
        private Theme _selectedTheme;
        private ObservableCollection<Theme> _themes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GeneralSettingsViewModel" /> class.
        /// </summary>
        public GeneralSettingsViewModel()
        {
            Title = "General";

            _themes = new ObservableCollection<Theme>();

            SaveCommand = new DelegateCommand(OnSave);
        }

        /// <summary>
        ///     Gets or sets the selected theme.
        /// </summary>
        /// <value>
        ///     The selected theme.
        /// </value>
        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set => Set(ref _selectedTheme, value);
        }

        /// <summary>
        ///     Gets or sets the themes.
        /// </summary>
        /// <value>
        ///     The themes.
        /// </value>
        public ObservableCollection<Theme> Themes
        {
            get => _themes;
            set => Set(ref _themes, value);
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

            try
            {
                RetryCount = Settings.Default.GeneralNetworkRetryCount;
                TimeSpanBetweenRetries = Settings.Default.GeneralNetworkTimeSpanBetweenRetries;
                TimeSpanBetweenAutoSaves = Settings.Default.AutoSaveTime;

                _themes = new ObservableCollection<Theme>
                {
                    new FluentTheme(),
                    new MaterialTheme(),
                    new Office2016Theme(),
                    new Office2016TouchTheme(),
                    new Windows7Theme(),
                    new Windows8Theme(),
                    new Windows8TouchTheme()
                };

                var currentThemeType = Settings.Default.GeneralAppearanceTheme.GetType();
                SelectedTheme = _themes.FirstOrDefault(s => s.GetType() == currentThemeType);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        ///     Called when [save].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnSave(object obj)
        {
            Settings.Default.GeneralAppearanceTheme = _selectedTheme;
            Settings.Default.GeneralNetworkRetryCount = RetryCount;
            Settings.Default.GeneralNetworkTimeSpanBetweenRetries = TimeSpanBetweenRetries;
            Settings.Default.AutoSaveTime = TimeSpanBetweenAutoSaves;
            Settings.Default.Save();

            RadMessageBoxService.Instance.Show("OneBurn", "OneBurn will restart now in order for the changes to take effect.", MessageBoxButton.OK, MessageBoxImage.Information);
            RadApplicationService.Instance.Restart();
        }
    }
}