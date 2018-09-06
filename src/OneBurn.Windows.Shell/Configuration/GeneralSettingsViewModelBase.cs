using System;
using System.Windows.Input;

namespace OneBurn.Windows.Shell.Configuration
{
    public abstract class GeneralSettingsViewModelBase : ContextViewModelBase
    {
        private int _retryCount;
        private TimeSpan _timeSpanBetweenRetries;
        private TimeSpan _timeSpanBetweenAutoSaves;

        /// <summary>
        ///     Gets or sets the retry count.
        /// </summary>
        /// <value>
        ///     The retry count.
        /// </value>
        public virtual int RetryCount
        {
            get => _retryCount;
            set => Set(ref _retryCount, value);
        }

        /// <summary>
        ///     Gets or sets the save command.
        /// </summary>
        /// <value>
        ///     The save command.
        /// </value>
        public virtual ICommand SaveCommand { get; protected set; }

        /// <summary>
        ///     Gets or sets the time span between retries.
        /// </summary>
        /// <value>
        ///     The time span between retries.
        /// </value>
        public virtual TimeSpan TimeSpanBetweenRetries
        {
            get => _timeSpanBetweenRetries;
            set => Set(ref _timeSpanBetweenRetries, value);
        }

        /// <summary>
        ///     Gets or sets the time span between automatic saves.
        /// </summary>
        /// <value>
        ///     The time span between automatic saves.
        /// </value>
        public virtual TimeSpan TimeSpanBetweenAutoSaves
        {
            get => _timeSpanBetweenAutoSaves;
            set => Set(ref _timeSpanBetweenAutoSaves, value);
        }
    }
}