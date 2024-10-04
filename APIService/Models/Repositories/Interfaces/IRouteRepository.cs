using Route = ClaimsService.DAL.Route;

namespace ClaimsService.Models.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        List<Route> GetAllRoutes(bool lazyLoad = true);
        List<Route> GetAllRoutesForClient(int clientId);
        Route GetRouteModelByRouteId(int routeId);
        bool UpdateRoute(Route route);        
        bool AddRoute(Route route);
        bool DeleteRoute(int routeId);
    }
}
