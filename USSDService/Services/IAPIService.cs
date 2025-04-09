using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace APIService.Services
{
    public interface IAPIService
    {
        List<Route> GetAllAPIRoutes(bool lazyLoad = true);
        Route GetAPIRouteById(int routeId);
        Route GetAPIRouteByName(string name);
        bool UpdateAPIRoute(SmartSwitchV2.Core.Shared.Entities.Route route);
        Task<Response> ProcessAPIRequest(Request request);
        Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate);
    }
}
