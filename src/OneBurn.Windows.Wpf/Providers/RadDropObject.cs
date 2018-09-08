using System.Collections.ObjectModel;
using OneBurn.Windows.Controls;
using OneBurn.Windows.Shell;

namespace OneBurn.Windows.Wpf.Providers
{
    public class RadDropObject : ViewModelBase, IDropObject
    {
        private ObservableCollection<object> _source;
        private object _target;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        public ObservableCollection<object> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the current dragged over item.
        /// </summary>
        /// <value>
        ///     The current dragged over item.
        /// </value>
        public object Target
        {
            get => _target;
            set => Set(ref _target, value);
        }
    }
}