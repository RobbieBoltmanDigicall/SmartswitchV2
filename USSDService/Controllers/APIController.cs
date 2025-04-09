using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartSwitchV2.Core.Shared.Entities;
using APIService.Services;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace APIService.Controllers
{
    [ApiController]
    [Route("API")]
    public class APIController : Controller
    {
        private readonly ILogger<APIController> _logger;
        private readonly IAPIService _apiService;

        public APIController(ILogger<APIController> logger,
            IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet("GetAllRoutes")]
        public List<Route> GetAllRoutes(bool lazyLoad)
        {
            return _apiService.GetAllAPIRoutes(lazyLoad);
        }

        [HttpGet("GetRouteById/{routeId}")]
        public Route GetRouteById(int routeId)
            => _apiService.GetAPIRouteById(routeId);

        [HttpPost("ProcessRequest")]
        public async Task<IActionResult> ProcessAPIRequest(Request request)
        {
            var result = await _apiService.ProcessAPIRequest(request);
            if (result.ResponseStatus == System.Net.HttpStatusCode.OK)
                return Ok(result);
            else 
                return BadRequest(result);
            
        }

        [HttpPost("Edit")]
        public IActionResult EditAPI(SmartSwitchV2.Core.Shared.Entities.Route route)
        {
            var result = _apiService.UpdateAPIRoute(route);

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("ReadLogs")]
        public async Task<IActionResult> ReadLogs(DateTime startDate, DateTime endDate)
        {
            var result = await _apiService.ReadLogs(startDate, endDate);

            return Ok(result);
        }
        
    }
}
