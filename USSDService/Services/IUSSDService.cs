using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace USSDService.Services
{
    public interface IUSSDService
    {
        List<Route> GetAllUSSDRoutes(bool lazyLoad = true);
        Route GetUSSDRouteById(int routeId);
        bool UpdateUSSDRoute(SmartSwitchV2.Core.Shared.Entities.Route route);
    }
}
