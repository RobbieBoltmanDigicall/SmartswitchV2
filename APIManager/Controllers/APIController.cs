﻿using APIManager.Models;
using APIManager.Services.Claims;
using APIManager.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

namespace APIManager.Controllers
{
    public class APIController : Controller
    {
        private readonly ILogger<APIController> _logger;
        private readonly IAPIService _apiService;

        public APIController(ILogger<APIController> logger,
            IAPIService apiService
            )
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> InsertUpdateAPI(int routeId)
        {
            try
            {
                RequestViewModel viewModel;
                viewModel = routeId != 0 ? await _apiService.GetAPIRouteById(routeId) 
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

                var listRoutesInSystem = await _apiService.ListAllAPIRoutes();                
                var routesSelectList = listRoutesInSystem
                    .Where(r => r.Route.RouteId != viewModel.Route.RouteId && !r.Route.RouteParentId.HasValue)
                    .Select(r => new SelectListItem() { Value = r.Route.RouteId.ToString(), Text = r.Route.RouteName}).ToList();

                viewModel.LinkedRouteNames = listRoutesInSystem.Where(r => r.Route.RouteParentId == viewModel.Route.RouteId).Select(r => r.Route.RouteName).ToList();
                viewModel.IsParent = listRoutesInSystem.Any(r => r.Route.RouteParentId == viewModel.Route.RouteId);
                viewModel.ListRoutesInSystem = new SelectList(routesSelectList);
                viewModel.RouteTypes = new SelectList(routeTypes);
                viewModel.MethodTypes = new SelectList(methodTypes);
                viewModel.DataTypes = new SelectList(dataTypes);
                viewModel.ApplicationTypes = new SelectList(applicationTypes);

                return View("~/Views/API/InsertUpdateAPI.cshtml", viewModel);
            }
            catch (Exception ex)
            {;
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateAPI(RequestViewModel requestViewModel)
        {
            var result = await _apiService.UpdateRequest(requestViewModel.Route);
            if (result)
                return await APIList();
            return BadRequest();
        }        

        public async Task<IActionResult> APIList()
        {
            var viewModel = await _apiService.ListAllAPIRoutes();
            return View("~/Views/API/APIList.cshtml", viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
