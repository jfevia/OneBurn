using System.Windows.Input;

namespace OneBurn.Windows.Shell.Configuration
{
    public abstract class WriteSettingsViewModelBase : ContextViewModelBase
    {
        private bool _rememberNumberOfCopies;

        /// <summary>
        ///     Gets or sets the save command.
        /// </summary>
        /// <value>
        ///     The save command.
        /// </value>
        public virtual ICommand SaveCommand { get; protected set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [remember number of copies].
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if [remember number of copies]; otherwise, <see langword="false" />.
        /// </value>
        public virtual bool RememberNumberOfCopies
        {
            get => _rememberNumberOfCopies;
            set => Set(ref _rememberNumberOfCopies, value);
        }
    }
}