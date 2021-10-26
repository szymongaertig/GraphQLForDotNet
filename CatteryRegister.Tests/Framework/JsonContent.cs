using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CatteryRegister.Tests.Framework
{
    public class JsonContent : StringContent
    {
        public JsonContent(string content)
            : base(content)
        {
        }

        public JsonContent(string content, Encoding encoding)
            : base(content, encoding)
        {
        }

        public JsonContent(string content, Encoding encoding, string mediaType)
            : base(content, encoding, mediaType)
        {
        }

        public JsonContent(object instance)
            : base(
                instance != null ? JsonSerializer.Serialize<object>(instance, JsonConfiguration.Options) : string.Empty,
                Encoding.UTF8, "application/json")
        {
        }
    }
}