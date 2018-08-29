﻿namespace OneBurn.Windows.Shell
{
    public interface IAwaitable
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; set; }
    }
}