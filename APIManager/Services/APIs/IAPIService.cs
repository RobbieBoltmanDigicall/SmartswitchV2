using APIManager.Models;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

namespace APIManager.Services.APIs
{
    public interface IAPIService
    {
        Task<List<RequestViewModel>> ListAllAPIRoutes(bool lazyLoad = true);
        Task<RequestViewModel> GetAPIRouteById(int apiRouteId);
        Task<bool> UpdateRequest(Route route);
        Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate);
    }
}
