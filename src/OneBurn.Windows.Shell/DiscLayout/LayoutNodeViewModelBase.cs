using System.Collections.ObjectModel;
using OneBurn.DiscLayout;
using OneBurn.Windows.Shell.FileSystem;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class LayoutNodeViewModelBase : ContextViewModelBase, ILayoutNode
    {
        private ObservableCollection<FileItemViewModelBase> _childFiles;
        private ObservableCollection<LayoutNodeViewModelBase> _childNodes;
        private string _name;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LayoutNodeViewModelBase" /> class.
        /// </summary>
        protected LayoutNodeViewModelBase()
        {
            _childNodes = new ObservableCollection<LayoutNodeViewModelBase>();
            _childFiles = new ObservableCollection<FileItemViewModelBase>();
        }

        /// <summary>
        ///     Gets or sets the child nodes.
        /// </summary>
        /// <value>
        ///     The child nodes.
        /// </value>
        public virtual ObservableCollection<LayoutNodeViewModelBase> ChildNodes
        {
            get => _childNodes;
            set => Set(ref _childNodes, value);
        }

        /// <summary>
        ///     Gets or sets the child files.
        /// </summary>
        /// <value>
        ///     The child files.
        /// </value>
        public virtual ObservableCollection<FileItemViewModelBase> ChildFiles
        {
            get => _childFiles;
            set => Set(ref _childFiles, value);
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