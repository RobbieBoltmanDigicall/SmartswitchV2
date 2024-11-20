using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace USSDService.Services
{
    public interface IUSSDService
    {
        List<Route> GetAllUSSDRoutes(bool lazyLoad = true);
        Route GetUSSDRouteById(int routeId);
        Route GetUSSDRouteByName(string name);
        bool UpdateUSSDRoute(SmartSwitchV2.Core.Shared.Entities.Route route);
        Task<Response> ProcessUSSDRequest(Request request);
        Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate);
    }
}
