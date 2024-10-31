using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace SmartSwitchV2.DataLayer.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        List<Route> GetAllRoutes(int systemId, bool lazyLoad = true);
        List<Route> GetAllRoutesForClient(int clientId);
        Route GetRouteModelByRouteId(int routeId);
        bool UpdateRoute(Route route);        
        bool AddRoute(Route route);
        bool DeleteRoute(int routeId);
    }
}
