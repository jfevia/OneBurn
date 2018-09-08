using System.Windows;
using System.Windows.Controls;
using OneBurn.DiscLayout;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal class LayoutNodeTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the root template.
        /// </summary>
        /// <value>
        /// The root template.
        /// </value>
        public DataTemplate RootTemplate { get; set; }

        /// <summary>
        /// Gets or sets the folder template.
        /// </summary>
        /// <value>
        /// The folder template.
        /// </value>
        public DataTemplate FolderTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the default template.
        /// </summary>
        /// <value>
        ///     The default template.
        /// </value>
        public DataTemplate DefaultTemplate { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>
        ///     Returns a <see cref="T:System.Windows.DataTemplate" /> or <see langword="null" />. The default value is
        ///     <see langword="null" />.
        /// </returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case ILayoutRoot _:
                    return RootTemplate;
                case ILayoutFolder _:
                    return FolderTemplate;
                default:
                    return DefaultTemplate;
            }
        }
    }
}