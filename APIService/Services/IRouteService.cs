using Route = ClaimsService.DAL.Route;

namespace ClaimsService.Services
{
    public interface IRouteService
    {
        List<Route> GetAllRoutes();
        Route GetRouteById(int routeId);
        List<Route> GetRoutesForClient(int clientId);
    }
}
