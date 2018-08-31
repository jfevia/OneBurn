using OneBurn.Windows.Shell.Burning;

namespace OneBurn.Windows.Wpf.Burning
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