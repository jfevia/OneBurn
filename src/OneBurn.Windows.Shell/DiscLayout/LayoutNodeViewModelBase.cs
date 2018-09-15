using System.Collections.ObjectModel;
using OneBurn.DiscLayout;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class LayoutNodeViewModelBase : ContextViewModelBase, ILayoutNode
    {
        private string _name;
        private string _path;

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public string Path
        {
            get => _path;
            set => Set(ref _path, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public virtual string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
    }
}