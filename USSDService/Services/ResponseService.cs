using SmartSwitchV2.DataLayer.Repositories.Interfaces;

namespace APIService.Services
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
