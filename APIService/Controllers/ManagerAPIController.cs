using APIService.Controllers;
using ClaimsService.Models;
using ClaimsService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route = ClaimsService.DAL.Route;

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

    }
}
