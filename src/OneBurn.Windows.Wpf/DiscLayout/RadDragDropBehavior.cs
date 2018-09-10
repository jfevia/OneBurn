using System.Windows;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal abstract class RadDragDropBehavior
    {
        public static readonly DependencyProperty DragTemplateProperty = DependencyProperty.RegisterAttached("DragTemplate", typeof(DataTemplate), typeof(RadDragDropBehavior));

        /// <summary>
        ///     Gets the drag template.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The template.</returns>
        public static DataTemplate GetDragTemplate(DependencyObject obj)
        {
            return (DataTemplate) obj.GetValue(DragTemplateProperty);
        }

        /// <summary>
        ///     Sets the drag template.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetDragTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DragTemplateProperty, value);
        }
    }
}