using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "ProcessUSSD")]
        public IActionResult ProcessUSSD()
        {
            return Ok();
        }
    }
}
