using Newtonsoft.Json;
using SpaceXAPI_MVC_UI.Models;
using System.Net.Http.Headers;

namespace SpaceXAPI_MVC_UI.Service
{
    public class StreamLaunchService
    {
        private static HttpClient _httpClient = new HttpClient();


        public StreamLaunchService()
        {
            _httpClient.BaseAddress = new Uri("https://api.spacexdata.com/v5/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<LaunchModel?> GetLaunchesWithStream()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"launches");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();

                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var jsonSerializer = new JsonSerializer();
                        LaunchModel? launches = jsonSerializer.Deserialize<LaunchModel>(jsonTextReader);
                        return launches;
                    }
                }
            }
        }
    }
}
