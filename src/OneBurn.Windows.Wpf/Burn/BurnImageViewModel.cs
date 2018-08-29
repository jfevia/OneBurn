using OneBurn.Windows.Shell.Burn;

namespace OneBurn.Windows.Wpf.Burn
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