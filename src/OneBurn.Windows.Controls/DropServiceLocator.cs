namespace OneBurn.Windows.Controls
{
    public static class DropServiceLocator
    {
        /// <summary>
        ///     Gets the current provider.
        /// </summary>
        /// <value>
        ///     The current provider.
        /// </value>
        public static IDropServiceProvider Current { get; private set; }

        /// <summary>
        ///     Sets the provider.
        /// </summary>
        /// <param name="newProvider">The new provider.</param>
        public static void SetProvider(IDropServiceProvider newProvider)
        {
            Current = newProvider;
        }
    }
}