using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OneBurn.Windows.Shell.FileSystem;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using GiveFeedbackEventArgs = Telerik.Windows.DragDrop.GiveFeedbackEventArgs;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    public partial class DiscLayoutEditorView
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Windows.Wpf.DiscLayout.DiscLayoutEditorView" /> class.
        /// </summary>
        public DiscLayoutEditorView()
        {
            InitializeComponent();

            InitializeDragDropEvents();
        }

        /// <summary>
        ///     Initializes the drag drop events.
        /// </summary>
        private void InitializeDragDropEvents()
        {
            // DirectoryFiles
            DragDropManager.AddDragInitializeHandler(RadGridViewDirectoryFiles, RadGridViewDirectoryFiles_DragInitialize);

            // Directories
            DragDropManager.AddDragInitializeHandler(RadTreeViewDirectories, RadTreeViewDirectories_DragInitialize);
            DragDropManager.AddGiveFeedbackHandler(RadTreeViewDirectories, RadItemsControl_GiveFeedback);

            // LayoutFiles
            DragDropManager.AddDropHandler(RadGridViewLayoutFiles, RadGridViewLayoutFiles_Drop);
            DragDropManager.AddDragOverHandler(RadGridViewLayoutFiles, RadGridViewLayoutFiles_DragOver);

            // DiscLayout
            DragDropManager.AddDropHandler(RadTreeViewDiscLayout, RadTreeViewDiscLayout_Drop);
            DragDropManager.AddDragOverHandler(RadTreeViewDiscLayout, RadTreeViewDiscLayout_DragOver);

            // Others
            DragDropManager.AddGiveFeedbackHandler(RadGridViewDirectoryFiles, RadItemsControl_GiveFeedback);
        }

        /// <summary>
        ///     Handles the DragOver event of the RadTreeViewDiscLayout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.DragDrop.DragEventArgs" /> instance containing the event data.</param>
        private static void RadTreeViewDiscLayout_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            if (DragDropPayloadManager.GetDataFromObject(e.Data, "Data") is DirectoryItemViewModelBase)
            {
                e.Effects = DragDropEffects.Copy;
                return;
            }

            e.Effects = DragDropEffects.None;
        }

        /// <summary>
        ///     Handles the Drop event of the RadTreeViewDiscLayout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private void RadTreeViewDiscLayout_Drop(object sender, DragEventArgs e)
        {
            if (!(RadTreeViewDiscLayout.ItemsSource is IList itemsSource))
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "Data") is DirectoryItemViewModelBase item))
                return;

            if (e.Effects == DragDropEffects.None)
                return;

            itemsSource.Add(item);
            e.Handled = true;
        }

        /// <summary>
        ///     Handles the DragInitialize event of the RadTreeViewDirectories control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragInitializeEventArgs" /> instance containing the event data.</param>
        private void RadTreeViewDirectories_DragInitialize(object sender, DragInitializeEventArgs e)
        {
            if (!(e.OriginalSource is RadTreeViewItem source))
                return;

            var payload = DragDropPayloadManager.GeneratePayload(null);
            payload.SetData("Data", source.Item as DirectoryItemViewModelBase);

            e.Data = payload;
            e.DragVisual = new DragVisual
            {
                ContentTemplate = RadDragDropBehavior.GetDragTemplate(RadTreeViewDirectories)
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        /// <summary>
        ///     Handles the GiveFeedback event of the RadItemsControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.DragDrop.GiveFeedbackEventArgs" /> instance containing the event data.</param>
        private static void RadItemsControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        /// <summary>
        ///     Handles the DragInitialize event of the RadGridViewDirectoryFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragInitializeEventArgs" /> instance containing the event data.</param>
        private void RadGridViewDirectoryFiles_DragInitialize(object sender, DragInitializeEventArgs e)
        {
            var payload = DragDropPayloadManager.GeneratePayload(null);
            payload.SetData("Data", RadGridViewDirectoryFiles.SelectedItems.Cast<FileItemViewModelBase>());

            e.Data = payload;
            e.DragVisual = new DragVisual
            {
                ContentTemplate = RadDragDropBehavior.GetDragTemplate(RadGridViewDirectoryFiles)
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        /// <summary>
        ///     Handles the Drop event of the RadGridViewLayoutFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.DragDrop.DragEventArgs" /> instance containing the event data.</param>
        private void RadGridViewLayoutFiles_Drop(object sender, DragEventArgs e)
        {
            if (!(RadGridViewLayoutFiles.ItemsSource is IList itemsSource))
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "Data") is IEnumerable<FileItemViewModelBase> items))
                return;

            if (e.Effects == DragDropEffects.None)
                return;

            foreach (var item in items)
                itemsSource.Add(item);

            e.Handled = true;
        }

        /// <summary>
        ///     Handles the DragOver event of the RadGridViewLayoutFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private static void RadGridViewLayoutFiles_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            if (DragDropPayloadManager.GetDataFromObject(e.Data, "Data") is IEnumerable<FileItemViewModelBase>)
            {
                e.Effects = DragDropEffects.Copy;
                return;
            }

            e.Effects = DragDropEffects.None;
        }
    }
}