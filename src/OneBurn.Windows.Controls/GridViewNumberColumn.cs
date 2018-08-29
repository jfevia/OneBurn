using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Telerik.Windows.Controls.GridView;
using GridViewColumn = Telerik.Windows.Controls.GridViewColumn;

namespace OneBurn.Windows.Controls
{
    public class GridViewNumberColumn : GridViewColumn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Creates the element for the cell in view mode.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="dataItem"></param>
        /// <returns>The element.</returns>
        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            var textBlock = cell.Content as TextBlock ?? new TextBlock();

            var index = DataControl.Items.IndexOf(dataItem);
            textBlock.Text = index != -1 ? $"{DataControl.Items.PageIndex * DataControl.Items.PageSize + index + 1}" : string.Empty;

            return textBlock;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Raises the <see cref="E:Telerik.Windows.Controls.GridViewColumn.PropertyChanged" /> event.
        /// </summary>
        /// <param name="args">
        ///     The <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName != "DataControl")
                return;

            if (DataControl?.Items != null)
            {
                DataControl.Items.CollectionChanged += (s, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Remove)
                        Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Refresh));
                };
            }
        }
    }
}