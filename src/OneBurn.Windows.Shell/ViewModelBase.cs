using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OneBurn.Windows.Shell.Messaging;
using OneBurn.Windows.Shell.Services;

namespace OneBurn.Windows.Shell
{
    public class ViewModelBase : ObservableObject, IDisposable, IAwaitable, IViewable, INotifyDataErrorInfo
    {
        private string _description;
        private bool _isBusy;
        private string _title;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBase" /> class.
        /// </summary>
        public ViewModelBase()
        {
            ValidationErrors = new Dictionary<string, HashSet<string>>();
        }

        /// <summary>
        ///     Gets the validation errors.
        /// </summary>
        /// <value>
        ///     The validation errors.
        /// </value>
        protected virtual IDictionary<string, HashSet<string>> ValidationErrors { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public virtual event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public virtual bool HasErrors => ValidationErrors.Count > 0;

        /// <inheritdoc />
        /// <summary>
        ///     Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property to retrieve validation errors for; or <see langword="null" /> or
        ///     <see cref="F:System.String.Empty" />, to retrieve entity-level errors.
        /// </param>
        /// <returns>
        ///     The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return ValidationErrors.ContainsKey(propertyName) ? ValidationErrors[propertyName] : null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        public virtual string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public virtual string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        /// <summary>
        ///     Forces the validation.
        /// </summary>
        public virtual void ForceValidation()
        {
        }

        /// <summary>
        ///     Initializes this instance asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        public virtual async Task InitializeAsync()
        {
            IsBusy = true;
            await LoadDataAsync();
            IsBusy = false;
        }

        /// <summary>
        ///     Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public virtual void LogException(Exception exception)
        {
            MessagingService.Instance.Send(new ExceptionMessage(exception));
        }

        /// <summary>
        ///     Loads the data asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        protected virtual async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                // Nothing
            });
        }

        /// <summary>
        ///     Raises the errors changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void Validate([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                return;

            var result = Validate(propertyName, out var validationErrors);
            if (!result)
            {
                ValidationErrors[propertyName] = validationErrors;
                RaiseErrorsChanged(propertyName);
            }
            else if (ValidationErrors.ContainsKey(propertyName))
            {
                ValidationErrors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        /// <summary>
        ///     Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="validationErrors">The validation errors.</param>
        /// <returns><c>true</c> if the validation is passed; otherwise, <c>false</c>.</returns>
        protected virtual bool Validate(string propertyName, out HashSet<string> validationErrors)
        {
            validationErrors = new HashSet<string>();
            return validationErrors.Count == 0;
        }
    }
}