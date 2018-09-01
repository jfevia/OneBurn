using System.Collections.ObjectModel;

namespace OneBurn.Windows.Shell.Containers
{
    public class SingleContainerViewModel : ContainerViewModelBase
    {
        private ObservableCollection<ContextViewModelBase> _viewModels;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Containers.SingleContainerViewModel" /> class.
        /// </summary>
        public SingleContainerViewModel()
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