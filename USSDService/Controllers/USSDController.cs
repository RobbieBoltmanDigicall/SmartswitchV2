using Microsoft.AspNetCore.Mvc;
using SmartSwitchV2.Core.Shared.Entities;
using USSDService.Services;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace USSDService.Controllers
{
    [ApiController]
    [Route("USSD")]
    public class USSDController : Controller
    {
        private readonly ILogger<USSDController> _logger;
        private readonly IUSSDService _ussdService;

        public USSDController(ILogger<USSDController> logger,
            IUSSDService ussdService)
        {
            _logger = logger;
            _ussdService = ussdService;
        }

        [HttpGet("GetAllRoutes")]
        public List<Route> GetAllRoutes(bool lazyLoad)
        {
            return _ussdService.GetAllUSSDRoutes(lazyLoad);
        }

        [HttpGet]
        [Route("GetRouteById/{routeId}")]
        public Route GetRouteById(int routeId)
            => _ussdService.GetUSSDRouteById(routeId);

        [HttpPost(Name = "ProcessRequest")]
        public IActionResult ProcessUSSDRequest(Request request)
        {
            return Ok();
        }

    }
}
