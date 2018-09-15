using System.Collections.Generic;

namespace OneBurn.Serialization
{
    public interface ISerializationSettings
    {
        /// <summary>
        ///     Gets the rules.
        /// </summary>
        /// <value>
        ///     The rules.
        /// </value>
        ICollection<Rule> Rules { get; }
    }
}