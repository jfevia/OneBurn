using System;

namespace OneBurn.Windows.Shell.Messaging
{
    public class ExceptionMessage : MessageBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExceptionMessage" /> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExceptionMessage(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        ///     Gets the exception.
        /// </summary>
        /// <value>
        ///     The exception.
        /// </value>
        public Exception Exception { get; }
    }
}