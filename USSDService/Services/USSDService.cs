using ClaimsService.Models;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using System.Text;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;
using SmartSwitchV2.Core.Shared.Entities;

namespace USSDService.Services
{
    public class USSDService : IUSSDService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IResponseRepository _responseRepository;

        public USSDService(IRouteRepository routeRepository, IResponseRepository responseRepository)
        {
            _routeRepository = routeRepository;
            _responseRepository = responseRepository;
        }

        public List<Route> GetAllUSSDRoutes(bool lazyLoad) =>        
            _routeRepository.GetAllRoutes(2, lazyLoad);
        

        public Route GetUSSDRouteById(int routeId) =>
            _routeRepository.GetRouteModelByRouteId(routeId);


        public async Task<bool> CallRoute(Request request)
        {
            //var _httpClient = new HttpClient();

            //var jsonPayload = JsonConvert.SerializeObject(request.ClaimsBody);
            //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            //// TODO: Add logging

            //// TODO: Get client's Claim URL here
            //var claimUrl = "";

            //var response = await _httpClient.PostAsync(claimUrl, content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    // TODO: log failure in error table
            //    throw new Exception($"Failed to send claim payload. Status code: {response.StatusCode}");
            //}
            return true;
        }
    }
}
