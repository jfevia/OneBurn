using System.Threading.Tasks;
using Newtonsoft.Json;
using OneBurn.Windows.Shell.DiscLayout;

namespace OneBurn.Windows.Wpf.Services
{
    internal class DiscLayoutSerializationService
    {
        /// <summary>
        ///     Serializes the layout asynchronously.
        /// </summary>
        /// <param name="layoutRoot">The layout root.</param>
        /// <returns>The serialized layout.</returns>
        public static async Task<string> SerializeAsync(LayoutNodeViewModelBase layoutRoot)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(layoutRoot, Formatting.Indented, new DiscLayoutJsonConverter()));
        }

        /// <summary>
        ///     Deserializes the layout asynchronously.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The deserialized layout.</returns>
        public static async Task<LayoutNodeViewModelBase> DeserializeAsync(string value)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<LayoutNodeViewModelBase>(value, new DiscLayoutJsonConverter()));
        }
    }
}