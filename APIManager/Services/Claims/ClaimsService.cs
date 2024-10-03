using APIManager.Controllers;
using APIManager.Models.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using Route = APIManager.Models.Claims.Route;

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

        public async Task<Route> GetClaimRouteById(int claimId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Route>> ListAllClaimRoutes()
        {
            var response = await _httpClient.GetAsync("GetAllRoutes");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GetAllRoutes unsuccessful");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var listRoutes = JsonConvert.DeserializeObject<List<Route>>(content);

            return listRoutes;
        }

        public async Task<List<Route>> ListAllClaimRoutesByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Client>> ListAllClients()
        {
            throw new NotImplementedException();
        }
    }
}
