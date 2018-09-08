using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using GiveFeedbackEventArgs = Telerik.Windows.DragDrop.GiveFeedbackEventArgs;

namespace OneBurn.Windows.Controls.GridView
{
    public class GridViewDragDropBehavior
    {
        private static readonly Dictionary<RadGridView, GridViewDragDropBehavior> Instances;

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(GridViewDragDropBehavior),
                new PropertyMetadata(OnIsEnabledPropertyChanged));

        public static readonly DependencyProperty DragDropEffectsProperty =
            DependencyProperty.RegisterAttached("DragDropEffects", typeof(DragDropEffects), typeof(GridViewDragDropBehavior));

        private RadGridView _associatedObject;

        /// <summary>
        ///     Initializes the <see cref="GridViewDragDropBehavior" /> class.
        /// </summary>
        static GridViewDragDropBehavior()
        {
            Instances = new Dictionary<RadGridView, GridViewDragDropBehavior>();
        }

        /// <summary>
        ///     Gets the is enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsEnabledProperty);
        }

        /// <summary>
        ///     Sets the is enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <see langword="true" /> [value].</param>
        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            var behavior = GetAttachedBehavior(obj as RadGridView);

            if (value)
                behavior.Initialize();
            else
                behavior.CleanUp();

            obj.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        ///     Gets the drag drop effects.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static DragDropEffects GetDragDropEffects(DependencyObject obj)
        {
            return (DragDropEffects) obj.GetValue(DragDropEffectsProperty);
        }

        /// <summary>
        ///     Sets the drag drop effects.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetDragDropEffects(DependencyObject obj, DragDropEffects value)
        {
            obj.SetValue(DragDropEffectsProperty, value);
        }

        /// <summary>
        ///     Called when [is enabled property changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool) e.NewValue);
        }

        /// <summary>
        ///     Gets the attached behavior.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <returns>The behavior.</returns>
        private static GridViewDragDropBehavior GetAttachedBehavior(RadGridView gridView)
        {
            if (!Instances.ContainsKey(gridView))
            {
                Instances[gridView] = new GridViewDragDropBehavior();
                Instances[gridView]._associatedObject = gridView;
            }

            return Instances[gridView];
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected virtual void Initialize()
        {
            UnsubscribeFromDragDropEvents();
            SubscribeToDragDropEvents();
        }

        /// <summary>
        ///     Cleans up.
        /// </summary>
        protected virtual void CleanUp()
        {
            UnsubscribeFromDragDropEvents();
        }

        /// <summary>
        ///     Subscribes to drag drop events.
        /// </summary>
        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(_associatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(_associatedObject, OnGiveFeedback);
            DragDropManager.AddDropHandler(_associatedObject, OnDrop);
            DragDropManager.AddDragDropCompletedHandler(_associatedObject, OnDragDropCompleted);
            DragDropManager.AddDragOverHandler(_associatedObject, OnDragOver);
        }

        /// <summary>
        ///     Unsubscribes from drag drop events.
        /// </summary>
        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(_associatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(_associatedObject, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(_associatedObject, OnDrop);
            DragDropManager.RemoveDragDropCompletedHandler(_associatedObject, OnDragDropCompleted);
            DragDropManager.RemoveDragOverHandler(_associatedObject, OnDragOver);
        }

        /// <summary>
        ///     Called when [drag initialize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragInitializeEventArgs" /> instance containing the event data.</param>
        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            if (!(sender is RadGridView targetGridView))
                return;

            var obj = DropServiceLocator.Current.CreateObject();
            obj.Source = targetGridView.SelectedItems;

            var dragPayload = DragDropPayloadManager.GeneratePayload(null);
            dragPayload.SetData("DragData", obj.Source);
            dragPayload.SetData("DropData", obj);

            e.Data = dragPayload;
            e.DragVisual = new DragVisual
            {
                Content = obj,
                ContentTemplate = _associatedObject.Resources["DraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        /// <summary>
        ///     Called when [give feedback].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GiveFeedbackEventArgs" /> instance containing the event data.</param>
        private static void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        /// <summary>
        ///     Called when [drag drop completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragDropCompletedEventArgs" /> instance containing the event data.</param>
        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            if (!(sender is RadGridView targetGridView))
                return;

            if (!(targetGridView.ItemsSource is IList targetItemsSource))
                return;

            if (e.Effects == DragDropEffects.None)
                return;

            var dragDropEffects = GetDragDropEffects(_associatedObject);
            if (!dragDropEffects.HasFlag(DragDropEffects.Move))
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "DragData") is ObservableCollection<object> draggedItems))
                return;

            foreach(var item in draggedItems)
                targetItemsSource.Remove(item);
        }

        /// <summary>
        ///     Called when [drop].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (!(sender is RadGridView targetGridView))
                return;

            if (!(targetGridView.ItemsSource is IList targetItemsSource))
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "DragData") is ObservableCollection<object> draggedItems))
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "DropData") is IDropObject))
                return;

            if (e.Effects != DragDropEffects.None)
            {
                foreach(var item in draggedItems)
                    targetItemsSource.Add(item);
            }

            e.Handled = true;
        }

        /// <summary>
        ///     Called when [drag over].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "DropData") is IDropObject dropObject))
                return;

            if (!_associatedObject.AllowDrop)
                e.Effects = DragDropEffects.None;
            else
                dropObject.Target = _associatedObject;

            e.Handled = true;
        }
    }
}