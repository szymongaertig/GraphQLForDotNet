using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatteryRegister.Services
{
    public class RandomCatImageClient
    {
        private readonly IHttpClientFactory _factory;

        public RandomCatImageClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<RandomCatImage> Search()
        {
            var response = await _factory.CreateClient()
                .GetAsync("https://api.thecatapi.com/v1/images/search");
            var result =
                await JsonSerializer.DeserializeAsync<RandomCatImage[]>(await response.Content.ReadAsStreamAsync());
            return result[0];
        }
    }

    public class RandomCatImage
    {
        public string id { get; set; }
        public string url { get; set; }
    }
}