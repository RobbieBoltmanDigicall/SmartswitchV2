using APIManager.Models;
using RequestViewModel = APIManager.Models.RequestViewModel;

namespace APIManager.Services.Claims
{
    public interface IClaimsService
    {
        Task<List<RequestViewModel>> ListAllClaimRoutes(bool lazyLoad = true);
        Task<List<RequestViewModel>> ListAllClaimRoutesByClientId(int clientId);
        Task<List<ClientViewModel>> ListAllClients();
        Task<RequestViewModel> GetClaimRouteById(int claimId);
    }
}
