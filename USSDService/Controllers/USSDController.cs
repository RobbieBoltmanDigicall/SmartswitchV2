using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("GetRouteById/{routeId}")]
        public Route GetRouteById(int routeId)
            => _ussdService.GetUSSDRouteById(routeId);

        [HttpPost("ProcessRequest")]
        public async Task<IActionResult> ProcessUSSDRequest(Request request)
        {
            var result = await _ussdService.ProcessUSSDRequest(request);
            if (result.ResponseStatus == System.Net.HttpStatusCode.OK)
                return Ok(result);
            else 
                return BadRequest(result);
            
        }

        [HttpPost("Edit")]
        public IActionResult EditUSSD(SmartSwitchV2.Core.Shared.Entities.Route route)
        {
            var result = _ussdService.UpdateUSSDRoute(route);

            if (result)
                return Ok();
            return BadRequest();
        }

    }
}
