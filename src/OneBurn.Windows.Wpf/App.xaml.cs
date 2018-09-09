using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using log4net;
using OneBurn.Windows.Shell.Messaging;
using OneBurn.Windows.Shell.Services;
using OneBurn.Windows.Wpf.Properties;
using OneBurn.Windows.Wpf.Services;
using OneBurn.Windows.Wpf.Shell;
using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf
{
    public partial class App
    {
        /// <inheritdoc />
        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Initialize();

            var viewModel = new MainWindowViewModel();
            await viewModel.InitializeAsync();

            var window = new MainWindow();
            window.DataContext = viewModel;
            window.Closed += OnWindowClosed;

            MainWindow = Window.GetWindow(window);
            window.Show();
        }

        /// <summary>
        ///     Called when [window closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WindowClosedEventArgs" /> instance containing the event data.</param>
        private static void OnWindowClosed(object sender, WindowClosedEventArgs e)
        {
            MessagingService.Instance.Send(new MainWindowClosedMessage());
        }

        /// <summary>
        ///     Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogException(e.Exception);
            e.Handled = true;

            RadMessageBoxService.Instance.Show("An error has occurred", "An unexpected error has occurred. The application will terminate now.", MessageBoxButton.OK, MessageBoxImage.Error);

#if !DEBUG
            Current.Shutdown();
#endif
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static void LogException(Exception exception)
        {
            var currentMethod = MethodBase.GetCurrentMethod();
            var log = LogManager.GetLogger(currentMethod.DeclaringType);
            log.Error(exception.Message, exception);
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Subscribe();

            RadThemeService.Instance.Apply(Settings.Default.GeneralAppearanceTheme);
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        private void Subscribe()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            MessagingService.Instance.Register<ExceptionMessage>(this, s => LogException(s.Exception));
            MessagingService.Instance.Register<MainWindowClosedMessage>(this, s => Current.Shutdown());
        }
    }
}