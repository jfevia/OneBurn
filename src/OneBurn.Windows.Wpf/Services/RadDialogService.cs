using System;
using System.Windows;
using Telerik.Windows.Controls;
using ViewModelBase = OneBurn.Windows.Shell.ViewModelBase;

namespace OneBurn.Windows.Wpf.Services
{
    internal class RadDialogService : IDialogService<RadWindow>
    {
        /// <summary>
        ///     Initializes the <see cref="RadDialogService" /> class.
        /// </summary>
        static RadDialogService()
        {
            if (Instance == null)
                Instance = new RadDialogService();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static RadDialogService Instance { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Shows this instance.
        /// </summary>
        /// <typeparam name="TDialog"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns>
        ///     The result.
        /// </returns>
        public bool? Show<TDialog, TViewModel>(TViewModel viewModel)
            where TDialog : RadWindow, new()
            where TViewModel : ViewModelBase
        {
            var window = new TDialog();
            window.Owner = Application.Current.MainWindow;
            window.DataContext = viewModel;
            window.ShowDialog();
            var result = window.DialogResult;
            window.Close();
            GC.Collect();
            return result;
        }
    }
}