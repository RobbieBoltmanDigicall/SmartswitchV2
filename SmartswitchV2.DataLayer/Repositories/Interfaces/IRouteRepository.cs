using SmartSwitchV2.DataLayer.HTTPDefinitions;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace SmartSwitchV2.DataLayer.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        List<Route> GetAllRoutes(int systemId, bool lazyLoad = true);
        List<Route> GetAllRoutesForClient(int clientId);
        Route GetRouteModelByRouteId(int routeId);
        Route GetRouteModelByRouteName(string name);
        bool InsertUpdateRoute(Route routeToUpdate);        
        bool DeleteRoute(int routeId);
        List<Route> GetLinkedRoutes(int routeParentId);
        void InsertLog(string type, string system, string requestURL, string message, string payload, string stackTrace = "", bool failed = false);
        List<Log> ReadLogs(DateTime startDate, DateTime endDate);
    }
}
