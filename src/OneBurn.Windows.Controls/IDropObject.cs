using System.Collections.ObjectModel;

namespace OneBurn.Windows.Controls
{
    public interface IDropObject
    {
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        ObservableCollection<object> Source { get; set; }

        /// <summary>
        ///     Gets or sets the target.
        /// </summary>
        /// <value>
        ///     The target.
        /// </value>
        object Target { get; set; }
    }
}