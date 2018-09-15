using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneBurn.Serialization;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    public class DiscLayoutContractResolver : DefaultContractResolver
    {
        private static readonly List<ISerializationSettings> Settings = new List<ISerializationSettings>();

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OneBurn.Windows.Wpf.DiscLayout.Serialization.DiscLayoutContractResolver" /> class.
        /// </summary>
        public DiscLayoutContractResolver()
        {
            Settings.Add(new ViewModelBaseSerializationSettings());
            Settings.Add(new LayoutFileViewModelBaseSerializationSettings());
        }

        /// <summary>
        ///     Determines whether [is empty collection] [the specified property].
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///     <see langword="true" /> if [is empty collection] [the specified property]; otherwise, <see langword="false" />.
        /// </returns>
        private static bool IsEmptyCollection(JsonProperty property, object target)
        {
            var value = property.ValueProvider.GetValue(target);
            if (value is ICollection collection && collection.Count == 0)
                return true;

            if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                return false;

            var countProp = property.PropertyType.GetProperty("Count");
            if (countProp == null)
                return false;

            var count = (int) countProp.GetValue(value, null);
            return count == 0;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given
        ///     <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>
        ///     A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given
        ///     <see cref="T:System.Reflection.MemberInfo" />.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var shouldSerialize = property.ShouldSerialize;
            property.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !IsEmptyCollection(property, obj);

            PropertyRule propertyRule = null;

            foreach (var setting in Settings)
            {
                var memberInfo = setting.GetType().BaseType;
                if (memberInfo == null)
                    continue;

                if (memberInfo.GenericTypeArguments[0] != member.DeclaringType)
                    continue;

                foreach (var rule in setting.Rules.OfType<PropertyRule>())
                {
                    if (rule.PropertyInfo.Name != member.Name)
                        continue;

                    propertyRule = rule;
                    break;
                }

                if (propertyRule != null)
                    break;
            }

            propertyRule?.Update(property);
            return property;
        }
    }
}