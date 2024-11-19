using APIManager.Models;
using APIManager.Services.Claims;
using APIManager.Services.USSDs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

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

        [HttpGet]
        public async Task<IActionResult> InsertUpdateUSSD(int routeId)
        {
            try
            {
                RequestViewModel viewModel;
                viewModel = routeId != 0 ? await _ussdService.GetUSSDRouteById(routeId) 
                    : new RequestViewModel() 
                    { 
                        Route = new Route()
                        {
                            RouteType = new RouteType() { RouteTypeName = "REST", RouteTypeId = 1},
                            MethodType = new MethodType() { MethodTypeId = 1, MethodTypeName = "GET"},
                            RouteBody = new RouteBody() 
                            { 
                                BodyType = new BodyType() { BodyTypeId = 1, BodyTypeName = "form-data"},
                                ApplicationType = new ApplicationType() { ApplicationTypeId = 1, ApplicationTypeName = "TEXT"},
                                RouteBodyParameters = new List<RouteBodyParameter>()                                
                            }
                        }
                    };

                var routeTypes = new List<SelectListItem>() {
                    new SelectListItem() { Text = "REST", Value = "1" },
                    new SelectListItem() { Text = "SOAP", Value = "2" }
                };

                var methodTypes = new List<SelectListItem>() {
                    new SelectListItem() { Text = "GET", Value = "1" },
                    new SelectListItem() { Text = "POST", Value = "2" },
                    new SelectListItem() { Text = "DELETE", Value = "3" },
                    new SelectListItem() { Text = "PUT", Value = "4" },
                    new SelectListItem() { Text = "PATCH", Value = "5" },
                    new SelectListItem() { Text = "HEAD", Value = "6" },
                    new SelectListItem() { Text = "OPTIONS", Value = "7" }
                };

                var applicationTypes = new List<SelectListItem>() {
                    new SelectListItem() { Text = "Text", Value = "1" },
                    new SelectListItem() { Text = "Javascript", Value = "2" },
                    new SelectListItem() { Text = "JSON", Value = "3" },
                    new SelectListItem() { Text = "HTML", Value = "4" },
                    new SelectListItem() { Text = "XML", Value = "5" },
                };

                var dataTypes = new List<SelectListItem>() {
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

                viewModel.RouteTypes = new SelectList(routeTypes);
                viewModel.MethodTypes = new SelectList(methodTypes);
                viewModel.DataTypes = new SelectList(dataTypes);
                viewModel.ApplicationTypes = new SelectList(applicationTypes);

                return View("~/Views/USSD/InsertUpdateUSSD.cshtml", viewModel);
            }
            catch (Exception ex)
            {;
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateUSSD(RequestViewModel requestViewModel)
        {
            var result = await _ussdService.UpdateRequest(requestViewModel.Route);
            if (result)
                return await USSDList();
            return BadRequest();
        }        

        public async Task<IActionResult> USSDList()
        {
            var viewModel = await _ussdService.ListAllUSSDRoutes();
            return View("~/Views/USSD/USSDList.cshtml", viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
