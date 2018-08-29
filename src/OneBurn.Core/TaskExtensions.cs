using System;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;

namespace OneBurn.Core
{
    public static class TaskExtensions
    {
        /// <summary>
        ///     Fires the and forget safe asynchronously.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="handler">The handler.</param>
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}