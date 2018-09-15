using OneBurn.Windows.Shell.DiscLayout;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    internal class LayoutFileViewModelBaseSerializationSettings : SerializationSettings<LayoutFileViewModelBase>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LayoutFileViewModelBaseSerializationSettings" /> class.
        /// </summary>
        public LayoutFileViewModelBaseSerializationSettings()
        {
            Property(s => s.DateModified).Ignore();
            Property(s => s.Icon).Ignore();
            Property(s => s.Size).Ignore();
            Property(s => s.TypeName).Ignore();
        }
    }
}