using Telerik.Windows.Controls;

namespace OneBurn.Windows.Wpf.Services
{
    internal interface IThemeService
    {
        /// <summary>
        ///     Applies this instance.
        /// </summary>
        /// <param name="theme">The theme.</param>
        void Apply(Theme theme);
    }
}