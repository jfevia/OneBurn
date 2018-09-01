using System.Windows.Input;
using OneBurn.Windows.Shell.Containers;

namespace OneBurn.Windows.Wpf.Containers
{
    public class CommandContainerViewModel : SingleContainerViewModel
    {
        /// <summary>
        ///     Gets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command { get; set; }
    }
}