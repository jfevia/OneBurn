using OneBurn.Windows.Shell.Burn;

namespace OneBurn.Windows.Wpf.Burn
{
    internal sealed class CreateImageFromFilesAndFoldersViewModel : CreateImageFromFilesAndFoldersViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateImageFromFilesAndFoldersViewModel" /> class.
        /// </summary>
        public CreateImageFromFilesAndFoldersViewModel()
        {
            Title = "Create from Files & Folders";
            Description = "Create an image from files and folders";
        }
    }
}