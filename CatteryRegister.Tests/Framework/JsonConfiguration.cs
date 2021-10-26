using System.Text.Json;
using System.Text.Json.Serialization;

namespace CatteryRegister.Tests.Framework
{
    public static class JsonConfiguration
    {
        static JsonConfiguration()
        {
            JsonConfiguration.Options.Converters.Add((JsonConverter) new JsonStringEnumConverter());
            JsonConfiguration.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        public static JsonSerializerOptions Options { get; } = new JsonSerializerOptions();
    }
}