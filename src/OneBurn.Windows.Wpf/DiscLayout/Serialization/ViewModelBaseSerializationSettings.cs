using OneBurn.Windows.Shell;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    internal class ViewModelBaseSerializationSettings : SerializationSettings<ViewModelBase>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBaseSerializationSettings" /> class.
        /// </summary>
        public ViewModelBaseSerializationSettings()
        {
            Property(s => s.Description).Ignore();
            Property(s => s.IsBusy).Ignore();
            Property(s => s.Title).Ignore();
            Property(s => s.HasErrors).Ignore();
        }
    }
}