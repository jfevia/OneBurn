using System.Windows.Input;

namespace OneBurn.Windows.Shell.Containers
{
    public abstract class ContainerViewModelBase : ContextViewModelBase
    {
        private ContextViewModelBase _currentViewModel;

        /// <summary>
        ///     Gets or sets the current view model.
        /// </summary>
        /// <value>
        ///     The current view model.
        /// </value>
        public virtual ContextViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        /// <summary>
        ///     Gets the set current view model command.
        /// </summary>
        /// <value>
        ///     The set current view model command.
        /// </value>
        public virtual ICommand SetCurrentViewModelCommand { get; protected set; }
    }
}