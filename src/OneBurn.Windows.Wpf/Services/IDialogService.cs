using OneBurn.Windows.Shell;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IDialogService<in TWindow>
    {
        /// <summary>
        ///     Shows this instance.
        /// </summary>
        /// <typeparam name="TDialog"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns>The result.</returns>
        bool? Show<TDialog, TViewModel>(TViewModel viewModel)
            where TDialog : TWindow, new()
            where TViewModel : ViewModelBase;
    }
}