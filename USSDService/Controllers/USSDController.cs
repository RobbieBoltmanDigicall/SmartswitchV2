using Microsoft.AspNetCore.Mvc;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace USSDService.Controllers
{
    [ApiController]
    [Route("USSD")]
    public class USSDController : Controller
    {
        private readonly ILogger<USSDController> _logger;

        public USSDController(ILogger<USSDController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "ProcessRequest")]
        public IActionResult ProcessUSSDRequest(Request request)
        {
            return Ok();
        }

    }
}
