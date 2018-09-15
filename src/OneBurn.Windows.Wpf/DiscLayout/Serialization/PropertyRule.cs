using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneBurn.Serialization;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    public abstract class PropertyRule : Rule
    {
        /// <summary>
        ///     Gets or sets the property information.
        /// </summary>
        /// <value>
        ///     The property information.
        /// </value>
        public MemberInfo PropertyInfo { get; protected set; }

        /// <summary>
        ///     Updates the specified contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        public void Update(JsonProperty contract)
        {
            var props = typeof(JsonProperty).GetProperties();
            foreach (var rule in RegisteredRules)
            {
                var property = props.FirstOrDefault(x => x.Name == rule.Key);
                if (property == null)
                    continue;

                var value = rule.Value;
                if (property.PropertyType == value.GetType())
                {
                    property.SetValue(contract, value);
                }
            }
        }
    }

    public class PropertyRule<TEntity, TProperty> : PropertyRule
    {
        public const string ConverterKey = "Converter";
        public const string PropertyNameKey = "PropertyName";
        public const string IgnoredKey = "Ignored";

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyRule{TEntity, TProperty}" /> class.
        /// </summary>
        /// <param name="prop">The property.</param>
        public PropertyRule(Expression<Func<TEntity, TProperty>> prop)
        {
            PropertyInfo = ((MemberExpression) prop.Body).Member;
        }

        /// <summary>
        ///     Assigns the specified converter.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <returns>The property rule.</returns>
        public PropertyRule<TEntity, TProperty> Converter(JsonConverter converter)
        {
            AddRule(ConverterKey, converter);
            return this;
        }

        /// <summary>
        ///     Assigns the specified name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property rule.</returns>
        public PropertyRule<TEntity, TProperty> Name(string propertyName)
        {
            AddRule(PropertyNameKey, propertyName);
            return this;
        }

        /// <summary>
        ///     Ignores the property.
        /// </summary>
        /// <returns>The property rule.</returns>
        public PropertyRule<TEntity, TProperty> Ignore()
        {
            AddRule(IgnoredKey, true);
            return this;
        }
    }
}