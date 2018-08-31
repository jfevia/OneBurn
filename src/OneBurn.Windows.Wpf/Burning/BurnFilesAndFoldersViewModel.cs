using OneBurn.Windows.Shell.Burning;

namespace OneBurn.Windows.Wpf.Burning
{
    internal sealed class BurnFilesAndFoldersViewModel : BurnFilesAndFoldersViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BurnFilesAndFoldersViewModel" /> class.
        /// </summary>
        public BurnFilesAndFoldersViewModel()
        {
            Title = "Burn Files & Folders";
            Description = "Burn files and folders to an optical drive";
        }
    }
}