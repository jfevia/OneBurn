using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OneBurn.Serialization;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    public class SerializationSettings<T> : ISerializationSettings
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets the rules.
        /// </summary>
        /// <value>
        ///     The rules.
        /// </value>
        public ICollection<Rule> Rules { get; } = new List<Rule>();

        /// <summary>
        ///     Properties the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="prop">The property.</param>
        /// <returns>The property rule.</returns>
        public PropertyRule<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> prop)
        {
            var rule = new PropertyRule<T, TProperty>(prop);
            Rules.Add(rule);
            return rule;
        }
    }
}