using Microsoft.AspNetCore.Mvc;
using APIManager.Services.Claims;
using APIManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> EditClaim(int routeId)
        {
            try
            {
                RequestViewModel viewModel = await _claimsService.GetClaimRouteById(routeId);

                //TODO: Dynamically retrieve this from DB
                viewModel.RouteTypes =
                viewModel.MethodTypes = new List<SelectListItem>() {
                    new SelectListItem() { Text = "GET", Value = "1" },
                    new SelectListItem() { Text = "POST", Value = "2" },
                    new SelectListItem() { Text = "DELETE", Value = "3" },
                    new SelectListItem() { Text = "PUT", Value = "4" },
                    new SelectListItem() { Text = "PATCH", Value = "5" },
                    new SelectListItem() { Text = "HEAD", Value = "6" },
                    new SelectListItem() { Text = "OPTIONS", Value = "7" }
                };
                return View("~/Views/Claims/EditClaim.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                var s = ex;
                return null;
            }
        }

        public async Task<IActionResult> ClientList()
        {
            return View();
        }
    }
}
