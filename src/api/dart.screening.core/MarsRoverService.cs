using dart.screening.common.models;
using dart.screening.core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace dart.screening.core
{
    public class MarsRoverService : IMarsRoverService
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly MarsRoverOptions _marsRoverOptions;
        public readonly ILogger<MarsRoverService> _logger;
        public MarsRoverService(IHttpClientFactory httpClientFactory, IOptions<MarsRoverOptions> marsRoverOptions, ILogger<MarsRoverService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _marsRoverOptions = marsRoverOptions.Value;
            _logger = logger;
        }

        public async Task<RoverPhotos?> GetPhotosByDate(string earthDate)
        {
            try
            {
                string marsHoverEndpoint = $"{_marsRoverOptions.Api_Url}?earth_date={earthDate}&api_key={_marsRoverOptions.Api_Key}";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, marsHoverEndpoint)
                {
                    Headers =
                    {
                        { HeaderNames.Accept, "application/json" }
                    }
                };

                var httpClient = _httpClientFactory.CreateClient("MarsRoverClient");
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
 
                string photoResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RoverPhotos>(photoResponse);

            }
            catch(HttpRequestException exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
    }
}