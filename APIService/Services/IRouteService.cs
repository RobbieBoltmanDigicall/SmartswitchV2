using Route = ClaimsService.DAL.Route;

namespace ClaimsService.Services
{
    public interface IRouteService
    {
        List<Route> GetAllRoutes(bool lazyLoad = true);
        Route GetRouteById(int routeId);
        List<Route> GetRoutesForClient(int clientId);
    }
}
