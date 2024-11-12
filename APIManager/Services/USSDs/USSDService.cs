using APIManager.Models;
using APIManager.Services.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
            var response = await _httpClient.GetAsync($"GetAllRoutes?lazyLoad={lazyLoad}");

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

        public async Task<RequestViewModel> GetUSSDRouteById(int ussdRouteId)
        {
            var response = await _httpClient.GetAsync($"GetRouteById/{ussdRouteId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GetRouteById unsuccessful");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var route = JsonConvert.DeserializeObject<Route>(content);

            var result = new RequestViewModel()
            {
                Route = route,
                Clients = route.Clients.Select(c => new ClientViewModel()
                {
                    ClientId = c.ClientId,
                    ClientName = c.ClientName
                }).ToList()
            };

            return result;
        }

        public async Task<bool> UpdateRequest(Route route)
        {

            try
            {
                //TODO: Set these dynamically instead of hardcoding
                route.Response = new SmartSwitchV2.Core.Shared.Entities.Response();
                if (route.RouteBody != null)
                {
                    route.RouteBody.ApplicationTypeId = 3;
                    route.RouteBody.ApplicationType = new SmartSwitchV2.Core.Shared.Entities.ApplicationType()
                    {
                        ApplicationTypeId = 3,
                        ApplicationTypeName = "JSON"
                    };
                }

                route.RouteBody?.RouteBodyParameters.ForEach(p => p.DataType = new SmartSwitchV2.Core.Shared.Entities.DataType() { DataTypeId = p.DataTypeId, DataTypeName = "" });
                route.RouteHeaders?.ForEach(h => h.DataType = new SmartSwitchV2.Core.Shared.Entities.DataType() { DataTypeId = h.DataTypeId, DataTypeName = "" });
                var serializedRoute = JsonConvert.SerializeObject(route);
                var jsonContent = new StringContent(serializedRoute, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Edit", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
