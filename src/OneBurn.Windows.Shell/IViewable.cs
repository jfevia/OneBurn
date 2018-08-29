namespace OneBurn.Windows.Shell
{
    public interface IViewable
    {
        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }
    }
}