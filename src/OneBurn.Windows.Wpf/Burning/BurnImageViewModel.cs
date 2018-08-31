using OneBurn.Windows.Shell.Burning;

namespace OneBurn.Windows.Wpf.Burning
{
    internal sealed class BurnImageViewModel : BurnImageViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BurnImageViewModel" /> class.
        /// </summary>
        public BurnImageViewModel()
        {
            Title = "Burn Image";
            Description = "Burn ISO files to an optical drive";
        }
    }
}