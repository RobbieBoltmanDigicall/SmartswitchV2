using Microsoft.AspNetCore.Mvc;
using APIManager.Services.Claims;
using APIManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APIManager.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(ILogger<ClaimsController> logger, 
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
                viewModel.DataTypes = new List<SelectListItem>() {
                    new SelectListItem() { Text = "Boolean", Value = "1" },
                    new SelectListItem() { Text = "Byte", Value = "2" },
                    new SelectListItem() { Text = "SByte", Value = "3" },
                    new SelectListItem() { Text = "Char", Value = "4" },
                    new SelectListItem() { Text = "Decimal", Value = "5" },
                    new SelectListItem() { Text = "Double", Value = "6" },
                    new SelectListItem() { Text = "Float", Value = "7" },
                    new SelectListItem() { Text = "Int", Value = "8" },
                    new SelectListItem() { Text = "UInt", Value = "9" },
                    new SelectListItem() { Text = "Long", Value = "10" },
                    new SelectListItem() { Text = "ULong", Value = "11" },
                    new SelectListItem() { Text = "Short", Value = "12" },
                    new SelectListItem() { Text = "UShort", Value = "13" },
                    new SelectListItem() { Text = "String", Value = "14" },
                    new SelectListItem() { Text = "Object", Value = "15" },
                    new SelectListItem() { Text = "DateTime", Value = "16" },
                    new SelectListItem() { Text = "Guid", Value = "17" },
                    new SelectListItem() { Text = "TimeSpan", Value = "18" },
                    new SelectListItem() { Text = "Uri", Value = "19" }
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
