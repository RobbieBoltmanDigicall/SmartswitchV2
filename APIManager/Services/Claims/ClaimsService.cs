using APIManager.Controllers;
using APIManager.Models.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using RequestViewModel = APIManager.Models.Claims.RequestViewModel;

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
            throw new NotImplementedException();
        }

        public async Task<List<RequestViewModel>> ListAllClaimRoutes()
        {
            var response = await _httpClient.GetAsync("GetAllRoutes");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GetAllRoutes unsuccessful");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var listRoutes = JsonConvert.DeserializeObject<List<RequestViewModel>>(content);

            return listRoutes;
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
