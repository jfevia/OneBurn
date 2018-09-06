using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OneBurn.Windows.Shell.Configuration;
using OneBurn.Windows.Wpf.Properties;
using RocketDivision.StarBurnX;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Configuration
{
    internal sealed class WriteSettingsViewModel : WriteSettingsViewModelBase
    {
        private STARBURN_WRITE_MODE _selectedWriteMode;
        private ObservableCollection<STARBURN_WRITE_MODE> _writeModes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WriteSettingsViewModel" /> class.
        /// </summary>
        public WriteSettingsViewModel()
        {
            Title = "Write";

            SaveCommand = new DelegateCommand(OnSave);
        }

        /// <summary>
        ///     Gets or sets the selected write mode.
        /// </summary>
        /// <value>
        ///     The selected write mode.
        /// </value>
        public STARBURN_WRITE_MODE SelectedWriteMode
        {
            get => _selectedWriteMode;
            set => Set(ref _selectedWriteMode, value);
        }

        /// <summary>
        ///     Gets or sets the write modes.
        /// </summary>
        /// <value>
        ///     The write modes.
        /// </value>
        public ObservableCollection<STARBURN_WRITE_MODE> WriteModes
        {
            get => _writeModes;
            set => Set(ref _writeModes, value);
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
                RememberNumberOfCopies = Settings.Default.BurnRememberNumberOfCopies;

                _writeModes = new ObservableCollection<STARBURN_WRITE_MODE>
                {
                    STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_AUTO,
                    STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_96,
                    STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_DAO_16,
                    STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_SAO,
                    STARBURN_WRITE_MODE.STARBURN_WRITE_MODE_TAO
                };

                SelectedWriteMode = _writeModes.FirstOrDefault(s => s == Settings.Default.BurnDefaultWriteMode);
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
            Settings.Default.BurnDefaultWriteMode = _selectedWriteMode;
            Settings.Default.BurnRememberNumberOfCopies = RememberNumberOfCopies;
            Settings.Default.Save();
        }
    }
}