using System.Collections.ObjectModel;

namespace OneBurn.Windows.Shell.Containers
{
    public abstract class CompositeContainerViewModelBase : ContainerViewModelBase
    {
        private ObservableCollection<ContextViewModelBase> _primaryViewModels;
        private ObservableCollection<ContextViewModelBase> _secondaryViewModels;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="CompositeContainerViewModelBase" /> class.
        /// </summary>
        protected CompositeContainerViewModelBase()
        {
            _primaryViewModels = new ObservableCollection<ContextViewModelBase>();
            _secondaryViewModels = new ObservableCollection<ContextViewModelBase>();
        }

        /// <summary>
        ///     Gets or sets the primary view models.
        /// </summary>
        /// <value>
        ///     The primary view models.
        /// </value>
        public virtual ObservableCollection<ContextViewModelBase> PrimaryViewModels
        {
            get => _primaryViewModels;
            set => Set(ref _primaryViewModels, value);
        }

        /// <summary>
        ///     Gets or sets the secondary view models.
        /// </summary>
        /// <value>
        ///     The secondary view models.
        /// </value>
        public virtual ObservableCollection<ContextViewModelBase> SecondaryViewModels
        {
            get => _secondaryViewModels;
            set => Set(ref _secondaryViewModels, value);
        }
    }
}