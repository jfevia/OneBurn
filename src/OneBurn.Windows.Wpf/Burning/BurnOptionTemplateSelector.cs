using System.Windows;
using System.Windows.Controls;
using OneBurn.Windows.Wpf.DiscLayout;

namespace OneBurn.Windows.Wpf.Burning
{
    internal class BurnOptionTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        ///     Gets or sets the burn image template.
        /// </summary>
        /// <value>
        ///     The burn image template.
        /// </value>
        public DataTemplate BurnImageTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the burn files and folders template.
        /// </summary>
        /// <value>
        ///     The burn files and folders template.
        /// </value>
        public DataTemplate BurnFilesAndFoldersTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the create image from media template.
        /// </summary>
        /// <value>
        ///     The create image from media template.
        /// </value>
        public DataTemplate CreateImageFromMediaTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the create image from files and folders template.
        /// </summary>
        /// <value>
        ///     The create image from files and folders template.
        /// </value>
        public DataTemplate CreateImageFromFilesAndFoldersTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the disc layout editor template.
        /// </summary>
        /// <value>
        ///     The disc layout editor template.
        /// </value>
        public DataTemplate DiscLayoutEditorTemplate { get; set; }

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
                case BurnImageViewModel _:
                    return BurnImageTemplate;
                case BurnFilesAndFoldersViewModel _:
                    return BurnFilesAndFoldersTemplate;
                case CreateImageFromMediaViewModel _:
                    return CreateImageFromMediaTemplate;
                case CreateImageFromFilesAndFoldersViewModel _:
                    return CreateImageFromFilesAndFoldersTemplate;
                case DiscLayoutEditorViewModel _:
                    return DiscLayoutEditorTemplate;
                default:
                    return DefaultTemplate;
            }
        }
    }
}