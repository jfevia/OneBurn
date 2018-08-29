using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OneBurn.Windows.Shell
{
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Raises the PropertyChanging event if needed.
        /// </summary>
        /// <remarks>
        ///     If the propertyName parameter
        ///     does not correspond to an existing property on the current class, an
        ///     exception is thrown in DEBUG configuration only.
        /// </remarks>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        public virtual void RaisePropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <returns>
        ///     True if the PropertyChanged event has been raised,
        ///     false otherwise. The event is not raised if the old
        ///     value is equal to the new value.
        /// </returns>
        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            RaisePropertyChanging(propertyName);

            field = newValue;

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="propertyName">
        ///     (optional) The name of the property that
        ///     changed.
        /// </param>
        /// <returns>
        ///     True if the PropertyChanged event has been raised,
        ///     false otherwise. The event is not raised if the old
        ///     value is equal to the new value.
        /// </returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return Set(propertyName, ref field, newValue);
        }
    }
}