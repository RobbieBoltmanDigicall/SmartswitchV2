using APIManager.Services.Claims;
using APIManager.Services.USSDs;
using Microsoft.AspNetCore.Mvc;

namespace APIManager.Controllers
{
    public class USSDController : Controller
    {
        private readonly ILogger<USSDController> _logger;
        private readonly IUSSDService _ussdService;

        public USSDController(ILogger<USSDController> logger,
            IUSSDService ussdService
            )
        {
            _logger = logger;
            _ussdService = ussdService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
