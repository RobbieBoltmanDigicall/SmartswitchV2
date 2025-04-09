using APIManager.Models;
using APIManager.Services.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

namespace APIManager.Services.APIs
{
    public class APIService : IAPIService
    {
        private readonly ILogger<APIService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public APIService(ILogger<APIService> logger, HttpClient httpClient, IConfiguration config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _httpClient.BaseAddress = new Uri(_config.GetValue<string>("AppSettings:APIServiceUrl"));
        }

        public async Task<List<RequestViewModel>> ListAllAPIRoutes(bool lazyLoad = true)
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

        public async Task<RequestViewModel> GetAPIRouteById(int apiRouteId)
        {
            var response = await _httpClient.GetAsync($"GetRouteById/{apiRouteId}");

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
                route.Response = new Response();

                if (route.RouteBody?.RouteBodyParameters != null && route.RouteBody.RouteBodyParameters.Count > 0)
                {
                    route.RouteBody.RouteBodyParameters.ForEach(p => p.DataType = new DataType() { DataTypeId = p.DataTypeId, DataTypeName = "" });
                    if (route.RouteBody.BodyType == null || route.RouteBody.BodyType.BodyTypeId == 0)
                        route.RouteBody.BodyType = new BodyType() { BodyTypeId = 3, BodyTypeName = "raw" };
                }
                else
                    route.RouteBody = null;
                route.RouteHeaders?.ForEach(h => h.DataType = new DataType() { DataTypeId = h.DataTypeId, DataTypeName = "" });
                route.RouteParameters?.ForEach(h => h.DataType = new DataType() { DataTypeId = h.DataTypeId, DataTypeName = "" });

                if (route.Clients == null)
                    route.Clients = new List<Client>();

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

        public async Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"ReadLogs?startDate={startDate}&endDate={endDate}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Read Logs unsuccessful");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var logs = JsonConvert.DeserializeObject<List<Log>>(content);

            return logs;
        }
    }
}
