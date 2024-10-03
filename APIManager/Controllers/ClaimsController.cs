using APIManager.Models.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using Route = APIManager.Models.Claims.Route;
using APIManager.Services.Claims;

namespace APIManager.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(ILogger<HomeController> logger, 
            IClaimsService claimsService
            )
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ClaimsList()
        {
            var viewModel = await _claimsService.ListAllClaimRoutes();
            return View("~/Views/Claims/ClaimsList.cshtml", viewModel);
        }

        public async Task<IActionResult> ClientList()
        {
            return View();
        }
    }
}
