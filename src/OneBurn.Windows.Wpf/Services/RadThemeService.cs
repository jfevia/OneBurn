using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Services
{
    internal class RadThemeService : IThemeService
    {
        /// <summary>
        ///     Initializes the <see cref="RadThemeService" /> class.
        /// </summary>
        static RadThemeService()
        {
            if (Instance == null)
                Instance = new RadThemeService();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static RadThemeService Instance { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Applies this instance.
        /// </summary>
        /// <param name="theme">The theme.</param>
        public void Apply(Theme theme)
        {
            StyleManager.ApplicationTheme = theme;
        }
    }
}