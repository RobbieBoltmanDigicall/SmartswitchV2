using ClaimsService.Models.Repositories.Interfaces;
using Route = ClaimsService.DAL.Route;

namespace ClaimsService.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IResponseRepository _responseRepository;

        public RouteService(IRouteRepository routeRepository, IResponseRepository responseRepository)
        {
            _routeRepository = routeRepository;
            _responseRepository = responseRepository;
        }

        public Route GetRouteById(int routeId) =>
            _routeRepository.GetRouteModelByRouteId(routeId);
        

        public List<Route> GetRoutesForClient(int clientId) =>
            _routeRepository.GetAllRoutesForClient(clientId);
    }
}
