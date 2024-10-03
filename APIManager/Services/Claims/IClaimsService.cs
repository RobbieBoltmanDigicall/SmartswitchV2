using APIManager.Models.Claims;
using Route = APIManager.Models.Claims.Route;

namespace APIManager.Services.Claims
{
    public interface IClaimsService
    {
        Task<List<Route>> ListAllClaimRoutes();
        Task<List<Route>> ListAllClaimRoutesByClientId(int clientId);
        Task<List<Client>> ListAllClients();
        Task<Route> GetClaimRouteById(int claimId);
    }
}
