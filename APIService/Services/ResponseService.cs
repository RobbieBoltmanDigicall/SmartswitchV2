using ClaimsService.Models.Repositories.Interfaces;

namespace ClaimsService.Services
{
    public class ResponseService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IResponseRepository _responseRepository;

        public ResponseService(IRouteRepository routeRepository, IResponseRepository responseRepository)
        {
            _routeRepository = routeRepository;
            _responseRepository = responseRepository;
        }
    }
}
