using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using OneBurn.Windows.Controls;
using Telerik.Windows.DragDrop;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using GiveFeedbackEventArgs = Telerik.Windows.DragDrop.GiveFeedbackEventArgs;

namespace OneBurn.Windows.Wpf.DiscLayout
{
    internal abstract class RadDragDropBehavior<TSource, TDestination> : DependencyObject
    {
        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        public TSource Source { get; set; }

        /// <summary>
        ///     Gets or sets the destination.
        /// </summary>
        /// <value>
        ///     The destination.
        /// </value>
        public TDestination Destination { get; set; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        ///     Initializes the component.
        /// </summary>
        public abstract void InitializeComponent();

        /// <summary>
        ///     Cleans up.
        /// </summary>
        public abstract void CleanUp();
    }

    internal abstract class RadDragDropBehavior<TBehavior, TSource, TDestination, TItem> : RadDragDropBehavior<TSource, TDestination>
        where TBehavior : RadDragDropBehavior<TSource, TDestination>, new()
        where TSource : FrameworkElement
        where TDestination : FrameworkElement
    {
        private static readonly Dictionary<TSource, TBehavior> Instances;

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TBehavior),
                new PropertyMetadata(OnIsEnabledPropertyChanged));

        public static readonly DependencyProperty DragDropEffectsProperty =
            DependencyProperty.RegisterAttached("DragDropEffects", typeof(DragDropEffects), typeof(TBehavior));

        public static readonly DependencyProperty DragTemplateProperty =
            DependencyProperty.RegisterAttached("DragTemplate", typeof(DataTemplate), typeof(TBehavior));

        private bool _isInitialized;

        /// <summary>
        ///     Initializes the <see cref="RadDragDropBehavior{TBehavior,TSource,TDestination,TItem}" /> class.
        /// </summary>
        static RadDragDropBehavior()
        {
            Instances = new Dictionary<TSource, TBehavior>();
        }

        /// <summary>
        ///     Called when [drag drop completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="DragDropCompletedEventArgs{TSource, TDestination, TItem}" /> instance
        ///     containing the event data.
        /// </param>
        protected virtual void OnDragDropCompleted(object sender, DragDropCompletedEventArgs<TSource, TDestination, TItem> e)
        {
            if (!e.DragDropEffects.HasFlag(DragDropEffects.Move))
                return;

            e.ItemsSource.Remove(e.Container.Source);
        }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragInitializeEventArgs" /> instance containing the event data.</param>
        /// <returns>The container.</returns>
        protected abstract IDragDropContainer<TSource, TDestination, TItem> GetContainer(object sender, DragInitializeEventArgs e);

        /// <summary>
        ///     Gets the drag drop effects.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The value.</returns>
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

            var behavior = GetAttachedBehavior(obj as TSource);
            behavior.InitializeComponent();
        }

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

            var behavior = GetAttachedBehavior(obj as TSource);
            behavior.InitializeComponent();
        }

        /// <summary>
        ///     Gets the is enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The value.</returns>
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
            obj.SetValue(IsEnabledProperty, value);

            var behavior = GetAttachedBehavior(obj as TSource);
            behavior.InitializeComponent();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes the component.
        /// </summary>
        public override void InitializeComponent()
        {
            if (_isInitialized)
                return;

            if (!Validate())
            {
                CleanUp();
                return;
            }

            Initialize();
            _isInitialized = true;
        }

        /// <summary>
        ///     Validates this instance.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        private bool Validate()
        {
            if (!GetIsEnabled(Source))
                return false;

            if (GetDragDropEffects(Source) == DragDropEffects.None)
                return false;

            if (GetDragTemplate(Source) == null)
                return false;

            return true;
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
        /// <param name="source">The source.</param>
        /// <returns>The attached behavior.</returns>
        private static TBehavior GetAttachedBehavior(TSource source)
        {
            if (!Instances.ContainsKey(source))
            {
                Instances[source] = new TBehavior();
                Instances[source].Source = source;
            }

            return Instances[source];
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            UnsubscribeFromDragDropEvents();
            SubscribeToDragDropEvents();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Cleans up.
        /// </summary>
        public override void CleanUp()
        {
            UnsubscribeFromDragDropEvents();
        }

        /// <summary>
        ///     Subscribes to drag drop events.
        /// </summary>
        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(Source, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(Source, OnGiveFeedback);
            DragDropManager.AddDropHandler(Source, OnDrop);
            DragDropManager.AddDragDropCompletedHandler(Source, OnDragDropCompleted);
            DragDropManager.AddDragOverHandler(Source, OnDragOver);
        }

        /// <summary>
        ///     Unsubscribes from drag drop events.
        /// </summary>
        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(Source, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(Source, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(Source, OnDrop);
            DragDropManager.RemoveDragDropCompletedHandler(Source, OnDragDropCompleted);
            DragDropManager.RemoveDragOverHandler(Source, OnDragOver);
        }

        /// <summary>
        ///     Called when [drag initialize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragInitializeEventArgs" /> instance containing the event data.</param>
        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var container = GetContainer(sender, e);
            if (container == null)
                return;

            var payload = DragDropPayloadManager.GeneratePayload(null);
            payload.SetData("Container", container);

            e.Data = payload;
            e.DragVisual = new DragVisual
            {
                Content = container,
                ContentTemplate = GetDragTemplate(Source)
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        /// <summary>
        ///     Called when [give feedback].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.GiveFeedbackEventArgs" /> instance containing the event data.</param>
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
            var itemsSource = GetItemsSource(sender as TDestination);
            if (itemsSource == null)
                return;

            if (e.Effects == DragDropEffects.None)
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "Container") is IDragDropContainer<TSource, TDestination, TItem> container))
                return;

            var dragDropEffects = GetDragDropEffects(Source);
            OnDragDropCompleted(sender, new DragDropCompletedEventArgs<TSource, TDestination, TItem>(itemsSource, container, dragDropEffects));
        }

        /// <summary>
        ///     Gets the items source.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>The items source.</returns>
        protected abstract IList GetItemsSource(TDestination destination);

        /// <summary>
        ///     Called when [drop].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DragEventArgs" /> instance containing the event data.</param>
        private void OnDrop(object sender, DragEventArgs e)
        {
            var itemsSource = GetItemsSource(sender as TDestination);
            if (itemsSource == null)
                return;

            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "Container") is IDragDropContainer<TSource, TDestination, TItem> container))
                return;

            OnDrop(sender, new DragDropCompletedEventArgs<TSource, TDestination, TItem>(itemsSource, container, e.Effects));
            e.Handled = true;
        }

        /// <summary>
        ///     Called when [drop].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="DragDropCompletedEventArgs{TSource, TDestination, TItem}" /> instance containing the event
        ///     data.
        /// </param>
        protected virtual void OnDrop(object sender, DragDropCompletedEventArgs<TSource, TDestination, TItem> e)
        {
            if (e.DragDropEffects == DragDropEffects.None)
                return;

            e.ItemsSource.Add(e.Container.Source);
        }

        /// <summary>
        ///     Called when [drag over].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.DragDrop.DragEventArgs" /> instance containing the event data.</param>
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (!(DragDropPayloadManager.GetDataFromObject(e.Data, "Container") is IDragDropContainer<TSource, TDestination, TItem> container))
                return;

            if (!(sender is TDestination destination))
                return;

            if (!destination.AllowDrop)
                e.Effects = DragDropEffects.None;
            else
                container.Destination = destination;

            e.Handled = true;
        }
    }
}