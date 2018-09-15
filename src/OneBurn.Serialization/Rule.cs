using System.Collections.Generic;
using System.Linq;

namespace OneBurn.Serialization
{
    public abstract class Rule
    {
        /// <summary>
        ///     Gets the rules.
        /// </summary>
        /// <value>
        ///     The rules.
        /// </value>
        private Dictionary<string, object> Rules { get; } = new Dictionary<string, object>();

        /// <summary>
        ///     Gets the registered rules.
        /// </summary>
        /// <value>
        ///     The registered rules.
        /// </value>
        protected IEnumerable<KeyValuePair<string, object>> RegisteredRules => Rules.AsEnumerable();

        /// <summary>
        ///     Adds the rule.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void AddRule(string key, object value)
        {
            if (Rules.ContainsKey(key))
                Rules.Add(key, value);
            else
                Rules[key] = value;
        }
    }
}