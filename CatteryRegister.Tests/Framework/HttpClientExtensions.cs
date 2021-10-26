using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CatteryRegister.Tests.Framework
{
    public static class HttpClientExtensions
    {
        public static async Task<bool> ContainsValidGraphQlSchema(this HttpClient httpClient)
        {
            var assembly = typeof(HttpClientExtensions).Assembly;
            using var streamReader =
                new StreamReader(
                    assembly.GetManifestResourceStream("CatteryRegister.Tests.Framework.SchemaQuery.json"));
            var queryBody = await streamReader.ReadToEndAsync();
            var content = new StringContent(queryBody);
            content.Headers.Remove("Content-Type");
            content.Headers.Add("Content-Type", "application/json");
            var result = await httpClient.PostAsync("/graphql", content);
            return result.IsSuccessStatusCode;
        }

        public static async Task<GraphQlResponse> InvokeGraphQlRequestWithVariables(this HttpClient client,
            string query,
            object? variables = null,
            string? operationName = null)
        {
            var result = await client.PostAsync("graphql", new JsonContent(new
            {
                query,
                variables,
                operationName
            }));
            var responseBody = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var response = JsonConvert.DeserializeObject<GraphQlResponse>(responseBody);
            return response;
        }
    }
}