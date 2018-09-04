using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private bool _eject;
        private bool _finalizeDisc;
        private Drive _selectedDrive;
        private string _selectedFilePath;
        private DriveSpeed _selectedWriteSpeed;
        private bool _shutdown;
        private bool _testDisc;
        private bool _verifyDisc;
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
        ///     Gets or sets the selected file path.
        /// </summary>
        /// <value>
        ///     The selected file path.
        /// </value>
        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set => Set(ref _selectedFilePath, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [finalize disc].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [finalize disc]; otherwise, <see langword="false" />.
        /// </value>
        public bool FinalizeDisc
        {
            get => _finalizeDisc;
            set => Set(ref _finalizeDisc, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [verify disc].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [verify disc]; otherwise, <see langword="false" />.
        /// </value>
        public bool VerifyDisc
        {
            get => _verifyDisc;
            set => Set(ref _verifyDisc, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BurnImageViewModel" /> is eject.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if eject; otherwise, <see langword="false" />.
        /// </value>
        public bool Eject
        {
            get => _eject;
            set => Set(ref _eject, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BurnImageViewModel" /> is shutdown.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if shutdown; otherwise, <see langword="false" />.
        /// </value>
        public bool Shutdown
        {
            get => _shutdown;
            set => Set(ref _shutdown, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [test disc].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [test disc]; otherwise, <see langword="false" />.
        /// </value>
        public bool TestDisc
        {
            get => _testDisc;
            set => Set(ref _testDisc, value);
        }

        /// <summary>
        ///     Burns the image asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        private async Task BurnAsync()
        {
            await RadOdaeService.Instance.Burn(_selectedDrive, _selectedFilePath, _selectedWriteSpeed, Settings.Default.BurnDefaultWriteMode, TestDisc);
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
            await LoadDrivesAsync();
            SelectFirstOrDefaultDrive();
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