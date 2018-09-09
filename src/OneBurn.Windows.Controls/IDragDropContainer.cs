namespace OneBurn.Windows.Controls
{
    public interface IDragDropContainer<TSource, TDestination, TItem>
    {
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        TSource Source { get; set; }

        /// <summary>
        ///     Gets or sets the destination.
        /// </summary>
        /// <value>
        ///     The destination.
        /// </value>
        TDestination Destination { get; set; }

        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        TItem Item { get; set; }
    }
}