using SpaceXAPILaunchesTake2.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml.Serialization;

namespace SpaceXAPILaunchesTake2.Services
{
    public class CRUDService 
    {
        private static HttpClient _httpClient = new HttpClient();

        public CRUDService()
        {
            _httpClient.BaseAddress = new Uri("https://api.spacexdata.com/v5/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }  
        public async Task GetResource()
        {
            HttpResponseMessage? response = await _httpClient.GetAsync("launches");
            response.EnsureSuccessStatusCode();

            var launches = new List<LaunchModel>();
            var content = await response.Content.ReadAsStringAsync();
            if (response?.Content?.Headers?.ContentType?.MediaType == "application/json")
            {
                launches = JsonSerializer.Deserialize<List<LaunchModel>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            }
            else
            {
                if (response?.Content?.Headers?.ContentType?.MediaType == "application/xml")
                {
                    var serializer = new XmlSerializer(typeof(List<LaunchModel>));
                    launches = serializer.Deserialize(new StringReader(content)) as List<LaunchModel>;
                }
            }
            // do other things with list if you want
        }

        public async Task<IEnumerable<LaunchModel>> GetResourceThroughHttpRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "launches");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var launches = JsonSerializer.Deserialize<IEnumerable<LaunchModel>>(content,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            return launches;
        }
    }
}
