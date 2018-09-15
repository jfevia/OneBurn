using System.Threading.Tasks;
using Newtonsoft.Json;
using OneBurn.Windows.Shell.DiscLayout;

namespace OneBurn.Windows.Wpf.DiscLayout.Serialization
{
    internal class DiscLayoutSerializationService
    {
        /// <summary>
        ///     Initializes the <see cref="DiscLayoutSerializationService" /> class.
        /// </summary>
        static DiscLayoutSerializationService()
        {
            if (Instance == null)
                Instance = new DiscLayoutSerializationService();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiscLayoutSerializationService" /> class.
        /// </summary>
        public DiscLayoutSerializationService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DiscLayoutContractResolver()
            };
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static DiscLayoutSerializationService Instance { get; }


        /// <summary>
        ///     Serializes the layout asynchronously.
        /// </summary>
        /// <param name="layoutRoot">The layout root.</param>
        /// <returns>The serialized layout.</returns>
        public async Task<string> SerializeAsync(LayoutRootViewModelBase layoutRoot)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(layoutRoot));
        }

        /// <summary>
        ///     Deserializes the layout asynchronously.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The deserialized layout.</returns>
        public async Task<LayoutRootViewModelBase> DeserializeAsync(string value)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<LayoutRootViewModelBase>(value));
        }
    }
}