using System.Diagnostics;
using System.Windows;

namespace OneBurn.Windows.Wpf.Services
{
    internal class RadApplicationService : IApplicationService
    {
        /// <summary>
        ///     Initializes the <see cref="RadApplicationService" /> class.
        /// </summary>
        static RadApplicationService()
        {
            if (Instance == null)
                Instance = new RadApplicationService();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static RadApplicationService Instance { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Restarts this instance.
        /// </summary>
        public void Restart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}