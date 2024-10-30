using APIManager.Services.Claims;
using System.Net.Http;

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

        public List<Route> GetAllUSSDs(bool lazyLoad = true)
        {
            throw new NotImplementedException();
        }

        public Route GetUSSDById(int ussdId)
        {
            throw new NotImplementedException();
        }
    }
}
