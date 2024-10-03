using APIManager.Models.Claims;
using RequestViewModel = APIManager.Models.Claims.RequestViewModel;

namespace APIManager.Services.Claims
{
    public interface IClaimsService
    {
        Task<List<RequestViewModel>> ListAllClaimRoutes();
        Task<List<RequestViewModel>> ListAllClaimRoutesByClientId(int clientId);
        Task<List<ClientViewModel>> ListAllClients();
        Task<RequestViewModel> GetClaimRouteById(int claimId);
    }
}
