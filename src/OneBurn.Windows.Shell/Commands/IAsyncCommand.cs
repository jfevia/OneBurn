using System.Threading.Tasks;
using System.Windows.Input;

namespace OneBurn.Windows.Shell.Commands
{
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if this instance can execute; otherwise, <see langword="false" />.
        /// </returns>
        bool CanExecute();

        /// <summary>
        ///     Executes asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        Task ExecuteAsync();
    }

    public interface IAsyncCommand<in T> : ICommand
    {
        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <see langword="true" /> if this instance can execute the specified parameter; otherwise, <see langword="false" />.
        /// </returns>
        bool CanExecute(T parameter);

        /// <summary>
        ///     Executes asynchronously.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The task.</returns>
        Task ExecuteAsync(T parameter);
    }
}