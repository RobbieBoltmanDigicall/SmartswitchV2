using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace ClaimsService.Services
{
    public interface IRouteService
    {
        List<Route> GetAllRoutes(bool lazyLoad = true);
        Route GetRouteById(int routeId);
        List<Route> GetRoutesForClient(int clientId);
    }
}
