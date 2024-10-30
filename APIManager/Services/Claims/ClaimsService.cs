using APIManager.Controllers;
using APIManager.Models;
using Newtonsoft.Json;
using System.Net.Http;
using SmartSwitchV2.Core.Shared.Entities;
using RequestViewModel = APIManager.Models.RequestViewModel;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;

namespace APIManager.Services.Claims
{
    public class ClaimsService : IClaimsService
    {
        private readonly ILogger<ClaimsService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ClaimsService(ILogger<ClaimsService> logger, HttpClient httpClient, IConfiguration config) 
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _httpClient.BaseAddress = new Uri(_config.GetValue<string>("AppSettings:ClaimsServiceUrl"));
        }

        public async Task<RequestViewModel> GetClaimRouteById(int claimId)
        {
            var response = await _httpClient.GetAsync($"GetRouteById/{claimId}");

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

        public async Task<List<RequestViewModel>> ListAllClaimRoutes(bool lazyLoad = true)
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

        public async Task<List<RequestViewModel>> ListAllClaimRoutesByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientViewModel>> ListAllClients()
        {
            throw new NotImplementedException();
        }
    }
}
