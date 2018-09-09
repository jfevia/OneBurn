using System;
using System.Collections;
using System.Windows;
using OneBurn.Windows.Controls;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    public class DragDropCompletedEventArgs<TSource, TDestination, TItem> : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Windows.Wpf.DiscLayout.DragDropCompletedEventArgs`2" />
        ///     class.
        /// </summary>
        /// <param name="itemsSource">The items source.</param>
        /// <param name="container">The container.</param>
        /// <param name="dragDropEffects"></param>
        public DragDropCompletedEventArgs(IList itemsSource, IDragDropContainer<TSource, TDestination, TItem> container, DragDropEffects dragDropEffects)
        {
            ItemsSource = itemsSource;
            Container = container;
            DragDropEffects = dragDropEffects;
        }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        public IDragDropContainer<TSource, TDestination, TItem> Container { get; }

        /// <summary>
        ///     Gets the drag drop effects.
        /// </summary>
        /// <value>
        ///     The drag drop effects.
        /// </value>
        public DragDropEffects DragDropEffects { get; }

        /// <summary>
        ///     Gets the items source.
        /// </summary>
        /// <value>
        ///     The items source.
        /// </value>
        public IList ItemsSource { get; }
    }
}