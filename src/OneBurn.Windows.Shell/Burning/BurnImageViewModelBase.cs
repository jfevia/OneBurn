namespace OneBurn.Windows.Shell.Burning
{
    public abstract class BurnImageViewModelBase : ContextViewModelBase
    {
        private bool _eject;
        private bool _finalizeDisc;
        private int _numberOfCopies;
        private string _selectedFilePath;
        private bool _shutdown;
        private bool _testDisc;
        private bool _verifyDisc;

        /// <summary>
        ///     Gets or sets the selected file path.
        /// </summary>
        /// <value>
        ///     The selected file path.
        /// </value>
        public virtual string SelectedFilePath
        {
            get => _selectedFilePath;
            set => Set(ref _selectedFilePath, value);
        }

        /// <summary>
        ///     Gets or sets the number of copies.
        /// </summary>
        /// <value>
        ///     The number of copies.
        /// </value>
        public virtual int NumberOfCopies
        {
            get => _numberOfCopies;
            set => Set(ref _numberOfCopies, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [finalize disc].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [finalize disc]; otherwise, <see langword="false" />.
        /// </value>
        public virtual bool FinalizeDisc
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
        public virtual bool VerifyDisc
        {
            get => _verifyDisc;
            set => Set(ref _verifyDisc, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BurnImageViewModelBase" /> is eject.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if eject; otherwise, <see langword="false" />.
        /// </value>
        public virtual bool Eject
        {
            get => _eject;
            set => Set(ref _eject, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BurnImageViewModelBase" /> is shutdown.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if shutdown; otherwise, <see langword="false" />.
        /// </value>
        public virtual bool Shutdown
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
        public virtual bool TestDisc
        {
            get => _testDisc;
            set => Set(ref _testDisc, value);
        }
    }
}