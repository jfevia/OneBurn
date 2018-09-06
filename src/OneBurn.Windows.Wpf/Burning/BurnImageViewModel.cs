using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using OneBurn.Windows.Shell.Burning;
using OneBurn.Windows.Shell.Commands;
using OneBurn.Windows.Wpf.Properties;
using OneBurn.Windows.Wpf.Services;
using RocketDivision.StarBurnX;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Burning
{
    internal sealed class BurnImageViewModel : BurnImageViewModelBase
    {
        private ObservableCollection<Drive> _drives;
        private bool _numberOfCopiesNotificationsSuspended;
        private Drive _selectedDrive;
        private DriveSpeed _selectedWriteSpeed;
        private DispatcherTimer _settingsCacheSaveTimer;
        private ObservableCollection<STARBURN_WRITE_MODE> _writeModes;
        private ObservableCollection<DriveSpeed> _writeSpeeds;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BurnImageViewModel" /> class.
        /// </summary>
        public BurnImageViewModel()
        {
            Title = "Burn Image";
            Description = "Burn ISO files to an optical drive";

            BrowseFilePathCommand = new DelegateCommand(BrowseFilePath);
            RefreshDrivesCommand = new AsyncCommand(ReloadDrivesAsync);
            LoadDriveSpeedsCommand = new AsyncCommand(LoadWriteSpeedsAsync);
            LoadDriveWriteModesCommand = new AsyncCommand(LoadWriteModesAsync);
            BurnCommand = new AsyncCommand(BurnAsync);
        }

        /// <summary>
        ///     Gets the load drive write modes command.
        /// </summary>
        /// <value>
        ///     The load drive write modes command.
        /// </value>
        public ICommand LoadDriveWriteModesCommand { get; }

        /// <summary>
        ///     Gets the burn command.
        /// </summary>
        /// <value>
        ///     The burn command.
        /// </value>
        public ICommand BurnCommand { get; }

        /// <summary>
        ///     Gets or sets the selected write speed.
        /// </summary>
        /// <value>
        ///     The selected write speed.
        /// </value>
        public DriveSpeed SelectedWriteSpeed
        {
            get => _selectedWriteSpeed;
            set => Set(ref _selectedWriteSpeed, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the number of copies.
        /// </summary>
        /// <value>
        ///     The number of copies.
        /// </value>
        public override int NumberOfCopies
        {
            get => base.NumberOfCopies;
            set
            {
                base.NumberOfCopies = value;

                if (!_numberOfCopiesNotificationsSuspended)
                    Settings.Default.BurnCustomNumberOfCopies = NumberOfCopies;
            }
        }

        /// <summary>
        ///     Gets or sets the selected drive.
        /// </summary>
        /// <value>
        ///     The selected drive.
        /// </value>
        public Drive SelectedDrive
        {
            get => _selectedDrive;
            set => Set(ref _selectedDrive, value);
        }

        /// <summary>
        ///     Gets the load drive speeds command.
        /// </summary>
        /// <value>
        ///     The load drive speeds command.
        /// </value>
        public ICommand LoadDriveSpeedsCommand { get; }

        /// <summary>
        ///     Gets the browse file path command.
        /// </summary>
        /// <value>
        ///     The browse file path command.
        /// </value>
        public ICommand BrowseFilePathCommand { get; }

        /// <summary>
        ///     Gets the refresh drives command.
        /// </summary>
        public ICommand RefreshDrivesCommand { get; }

        /// <summary>
        ///     Gets or sets the drives.
        /// </summary>
        /// <value>
        ///     The drives.
        /// </value>
        public ObservableCollection<Drive> Drives
        {
            get => _drives;
            private set => Set(ref _drives, value);
        }

        /// <summary>
        ///     Gets or sets the write speeds.
        /// </summary>
        /// <value>
        ///     The write speeds.
        /// </value>
        public ObservableCollection<DriveSpeed> WriteSpeeds
        {
            get => _writeSpeeds;
            private set => Set(ref _writeSpeeds, value);
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

        /// <summary>
        ///     Burns the image asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task BurnAsync()
        {
            ForceValidation();
            if (HasErrors)
            {
                RadMessageBoxService.Instance.Show("Input Required", "Please, complete the required fields.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var count = NumberOfCopies;
            while (--count >= 0)
            {
                await RadOdaeService.Instance.Burn(_selectedDrive, SelectedFilePath, _selectedWriteSpeed, Settings.Default.BurnDefaultWriteMode, TestDisc);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validationErrors">The validation errors.</param>
        /// <returns>
        ///     <c>true</c> if the validation is passed; otherwise, <c>false</c>.
        /// </returns>
        protected override bool Validate(string propertyName, out HashSet<string> validationErrors)
        {
            validationErrors = new HashSet<string>();

            switch (propertyName)
            {
                case nameof(SelectedFilePath):
                    ValidateSelectedFilePath(validationErrors);
                    break;
                case nameof(SelectedDrive):
                    ValidateSelectedDrive(validationErrors);
                    break;
                case nameof(SelectedWriteSpeed):
                    ValidateSelectedWriteSpeed(validationErrors);
                    break;
                case nameof(NumberOfCopies):
                    ValidateNumberOfCopies(validationErrors);
                    break;
            }

            return validationErrors.Count == 0;
        }

        /// <summary>
        ///     Validates the number of copies.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        private void ValidateNumberOfCopies(ICollection<string> validationErrors)
        {
            if (NumberOfCopies <= 0)
                validationErrors.Add("The number of copies must be at least one");
        }

        /// <summary>
        ///     Validates the selected write speed.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        private void ValidateSelectedWriteSpeed(ICollection<string> validationErrors)
        {
            if (_selectedWriteSpeed == null)
                validationErrors.Add("Select the drive to use in the burning process");
        }

        /// <summary>
        ///     Validates the selected drive.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        private void ValidateSelectedDrive(ICollection<string> validationErrors)
        {
            if (_selectedDrive == null)
                validationErrors.Add("Select the drive to use in the burning process");
        }

        /// <inheritdoc />
        /// <summary>
        ///     Forces the validation.
        /// </summary>
        public override void ForceValidation()
        {
            Validate(nameof(SelectedFilePath));
            Validate(nameof(SelectedDrive));
            Validate(nameof(SelectedWriteSpeed));
            Validate(nameof(NumberOfCopies));
        }

        /// <summary>
        ///     Validates the selected file path.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        private void ValidateSelectedFilePath(ICollection<string> validationErrors)
        {
            if (string.IsNullOrWhiteSpace(SelectedFilePath))
                validationErrors.Add("Select a file to burn");
        }

        /// <summary>
        ///     Loads the write speeds asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task LoadWriteSpeedsAsync()
        {
            if (_selectedDrive == null)
                return;

            WriteSpeeds = new ObservableCollection<DriveSpeed>(await RadOdaeService.Instance.GetDriveWriteSpeedsAsync(_selectedDrive.DriveInfo));
        }

        /// <summary>
        ///     Loads the write modes asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task LoadWriteModesAsync()
        {
            WriteModes = new ObservableCollection<STARBURN_WRITE_MODE>(await RadOdaeService.Instance.GetDriveWriteModesAsync(_selectedDrive.DriveInfo));
        }

        /// <summary>
        ///     Browses the file path.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void BrowseFilePath(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ISO Files (*.iso)|*.iso";

            if (openFileDialog.ShowDialog() == true)
                SelectedFilePath = openFileDialog.FileName;
        }

        /// <summary>
        ///     Reloads the drives asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task ReloadDrivesAsync()
        {
            IsBusy = true;
            await LoadDrivesAsync();
            SelectFirstOrDefaultDrive();
            IsBusy = false;
        }

        /// <summary>
        ///     Selects the first or default drive.
        /// </summary>
        private void SelectFirstOrDefaultDrive()
        {
            SelectedDrive = _drives.FirstOrDefault();
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

            if (!Settings.Default.BurnRememberNumberOfCopies)
            {
                NumberOfCopies = Settings.Default.BurnDefaultNumberOfCopies;
            }
            else
            {
                SuspendNumberOfCopiesNotifications();
                NumberOfCopies = Settings.Default.BurnCustomNumberOfCopies;
                ResumeNumberOfCopiesNotifications();
            }

            await LoadDrivesAsync();
            SelectFirstOrDefaultDrive();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes this instance asynchronously.
        /// </summary>
        /// <returns>
        ///     The task.
        /// </returns>
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (Settings.Default.BurnRememberNumberOfCopies && _settingsCacheSaveTimer == null)
            {
                _settingsCacheSaveTimer = new DispatcherTimer();
                _settingsCacheSaveTimer.Interval = Settings.Default.AutoSaveTime;
                _settingsCacheSaveTimer.Tick += AutoSaveSettings;
                _settingsCacheSaveTimer.Start();
            }
            else if (!Settings.Default.BurnRememberNumberOfCopies && _settingsCacheSaveTimer != null)
            {
                _settingsCacheSaveTimer.Stop();
                _settingsCacheSaveTimer.Tick -= AutoSaveSettings;
                _settingsCacheSaveTimer = null;
            }
        }

        /// <summary>
        ///     Automatics the save settings.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private static void AutoSaveSettings(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        /// <summary>
        ///     Resumes the number of copies notifications.
        /// </summary>
        private void ResumeNumberOfCopiesNotifications()
        {
            _numberOfCopiesNotificationsSuspended = false;
        }

        /// <summary>
        ///     Suspends the number of copies notifications.
        /// </summary>
        private void SuspendNumberOfCopiesNotifications()
        {
            _numberOfCopiesNotificationsSuspended = true;
        }

        /// <summary>
        ///     Loads the drives asynchronously
        /// </summary>
        /// <returns>The task.</returns>
        private async Task LoadDrivesAsync()
        {
            Drives = new ObservableCollection<Drive>(await RadOdaeService.Instance.GetDrivesAsync());
        }
    }
}