using APIManager.Models;
using APIManager.Services.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

namespace APIManager.Services.USSDs
{
    public class USSDService : IUSSDService
    {
        private readonly ILogger<USSDService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public USSDService(ILogger<USSDService> logger, HttpClient httpClient, IConfiguration config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _httpClient.BaseAddress = new Uri(_config.GetValue<string>("AppSettings:USSDServiceUrl"));
        }

        public async Task<List<RequestViewModel>> ListAllUSSDRoutes(bool lazyLoad = true)
        {
            var response = await _httpClient.GetAsync($"GetAllUSSDRoutes?lazyLoad={lazyLoad}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GetAllRoutes unsuccessful");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var listRoutes = JsonConvert.DeserializeObject<List<Route>>(content);

            if (listRoutes == null)
            {
                _logger.LogError("listRoutes is null");
                return null;
            }

            var result = listRoutes.Select(route => new RequestViewModel
            {
                Route = route,
                Clients = route.Clients.Select(client => new ClientViewModel
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName
                }).ToList()
            }).ToList();


            return result;
        }

        public async Task<Route> GetUSSDRouteById(int ussdId)
        {
            throw new NotImplementedException();
        }
    }
}
