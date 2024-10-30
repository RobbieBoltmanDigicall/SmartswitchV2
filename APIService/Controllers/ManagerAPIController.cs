using APIService.Controllers;
using ClaimsService.Services;
using Microsoft.AspNetCore.Mvc;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace ClaimsService.Controllers
{
    [Route("ManagerAPI")]
    [ApiController]
    public class ManagerAPIController : Controller
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IRouteService _routeService;

        public ManagerAPIController(ILogger<ClaimsController> logger, IRouteService routeService)
        {
            _logger = logger;
            _routeService = routeService;
        }


        [HttpGet("GetAllRoutes")]
        public List<Route> GetAllRoutes(bool lazyLoad)
        {
            return _routeService.GetAllRoutes(lazyLoad);
        }

        [HttpGet]
        [Route("GetRouteById/{routeId}")]
        public Route GetRouteById(int routeId)
           => _routeService.GetRouteById(routeId);

    }
}
