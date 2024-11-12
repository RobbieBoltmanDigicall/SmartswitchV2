using ClaimsService.Models;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using System.Text;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;
using SmartSwitchV2.Core.Shared.Entities;
using SmartSwitchV2.Core.Shared.Utilities;

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

        public bool UpdateUSSDRoute(SmartSwitchV2.Core.Shared.Entities.Route route)
        {
            var routeToUpdate = ClassConverter.RouteConvert(route);

            return _routeRepository.UpdateRoute(routeToUpdate);
        }

        public Route GetUSSDRouteByName(string name)
        => _routeRepository.GetRouteModelByRouteName(name);        

        public async Task<Response> ProcessUSSDRequest(Request request)
        {
            var route = _routeRepository.GetRouteModelByRouteName(request.RouteName);


            if (route.RouteType.RouteTypeName == "REST")
            {
                var httpRequest = RequestMapper.MapRouteToHTTPRequest(route, request);
                HttpClient httpClient = new();
                var response = httpClient.Send(httpRequest);

                return new Response()
                {
                    ResponseContent = await response.Content.ReadAsStringAsync(),
                    ResponseStatus = response.StatusCode
                };
            }

            return new Response();
        }
    }
}
