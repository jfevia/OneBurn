using OneBurn.Windows.Controls;

namespace OneBurn.Windows.Wpf.Providers
{
    internal class RadDropServiceProvider : IDropServiceProvider
    {
        /// <inheritdoc />
        /// <summary>
        ///     Creates the object.
        /// </summary>
        /// <returns>
        ///     The object.
        /// </returns>
        public IDropObject CreateObject()
        {
            return new RadDropObject();
        }
    }
}