using System;
using System.Windows.Media;
using OneBurn.DiscLayout;
using OneBurn.FileSystem;

namespace OneBurn.Windows.Shell.DiscLayout
{
    public abstract class LayoutFileViewModelBase : LayoutNodeViewModelBase, ILayoutFile, IFileItem
    {
        private DateTime _dateModified;
        private ImageSource _icon;
        private long _size;
        private string _typeName;

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public virtual ImageSource Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the date modified.
        /// </summary>
        /// <value>
        ///     The date modified.
        /// </value>
        public virtual DateTime DateModified
        {
            get => _dateModified;
            set => Set(ref _dateModified, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>
        ///     The name of the type.
        /// </value>
        public virtual string TypeName
        {
            get => _typeName;
            set => Set(ref _typeName, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        public virtual long Size
        {
            get => _size;
            set => Set(ref _size, value);
        }
    }
}