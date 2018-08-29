using System.Collections.ObjectModel;

namespace OneBurn.Windows.Shell.Containers
{
    public abstract class SingleContainerViewModelBase : ContainerViewModelBase
    {
        private ObservableCollection<ContextViewModelBase> _viewModels;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Containers.SingleContainerViewModelBase" /> class.
        /// </summary>
        protected SingleContainerViewModelBase()
        {
            _viewModels = new ObservableCollection<ContextViewModelBase>();
        }

        /// <summary>
        ///     Gets or sets the view models.
        /// </summary>
        /// <value>
        ///     The view models.
        /// </value>
        public virtual ObservableCollection<ContextViewModelBase> ViewModels
        {
            get => _viewModels;
            set => Set(ref _viewModels, value);
        }
    }
}