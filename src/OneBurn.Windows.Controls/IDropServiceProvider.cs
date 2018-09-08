namespace OneBurn.Windows.Controls
{
    public interface IDropServiceProvider
    {
        /// <summary>
        ///     Creates the object.
        /// </summary>
        /// <returns>The object.</returns>
        IDropObject CreateObject();
    }
}