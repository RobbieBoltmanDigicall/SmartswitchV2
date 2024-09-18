using APIManager.Models;
using APIManager.Models.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using APIManager.Models.Claims;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace APIManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ClaimsList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5003/ManagerAPI/GetAllRoutes");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error retrieving data from the external API.");
            }

            var content = await response.Content.ReadAsStringAsync();
            //var content = "[\r\n  {\r\n    \"id\": 0,\r\n    \"routeType\": {\r\n      \"id\": 0,\r\n      \"routeTypeName\": \"string\",\r\n      \"route\": \"string\"\r\n    },\r\n    \"routePath\": \"string\",\r\n    \"order\": 0,\r\n    \"routeHeaders\": [\r\n      {\r\n        \"id\": 0,\r\n        \"route\": \"string\",\r\n        \"headerKey\": \"string\",\r\n        \"headerValue\": \"string\",\r\n        \"dataType\": {\r\n          \"id\": 0,\r\n          \"dataTypeName\": \"string\"\r\n        }\r\n      }\r\n    ],\r\n    \"routeParameters\": [\r\n      {\r\n        \"id\": 0,\r\n        \"route\": \"string\",\r\n        \"parameterKey\": \"string\",\r\n        \"parameterValue\": \"string\",\r\n        \"dataType\": {\r\n          \"id\": 0,\r\n          \"dataTypeName\": \"string\"\r\n        }\r\n      }\r\n    ],\r\n    \"routeBody\": {\r\n      \"id\": 0,\r\n      \"route\": \"string\",\r\n      \"bodyType\": {\r\n        \"id\": 0,\r\n        \"bodyTypeName\": \"string\"\r\n      },\r\n      \"applicationType\": {\r\n        \"id\": 0,\r\n        \"applicationTypeName\": \"string\"\r\n      },\r\n      \"routeBodyParameters\": [\r\n        {\r\n          \"id\": 0,\r\n          \"routeBody\": \"string\",\r\n          \"bodyKey\": \"string\",\r\n          \"bodyValue\": \"string\",\r\n          \"dataType\": {\r\n            \"id\": 0,\r\n            \"dataTypeName\": \"string\"\r\n          }\r\n        }\r\n      ]\r\n    },\r\n    \"methodType\": {\r\n      \"id\": 0,\r\n      \"methodTypeName\": \"string\",\r\n      \"route\": \"string\"\r\n    },\r\n    \"clients\": [\r\n      {\r\n        \"id\": 0,\r\n        \"clientName\": \"string\",\r\n        \"routes\": [\r\n          \"string\"\r\n        ]\r\n      }\r\n    ],\r\n    \"response\": {\r\n      \"id\": 0,\r\n      \"route\": \"string\",\r\n      \"responseBody\": {\r\n        \"id\": 0,\r\n        \"response\": \"string\",\r\n        \"bodyType\": {\r\n          \"id\": 0,\r\n          \"bodyTypeName\": \"string\"\r\n        },\r\n        \"applicationType\": {\r\n          \"id\": 0,\r\n          \"applicationTypeName\": \"string\"\r\n        },\r\n        \"parameters\": [\r\n          {\r\n            \"id\": 0,\r\n            \"responseBody\": \"string\",\r\n            \"key\": \"string\",\r\n            \"value\": \"string\",\r\n            \"dataType\": {\r\n              \"id\": 0,\r\n              \"dataTypeName\": \"string\"\r\n            }\r\n          }\r\n        ]\r\n      },\r\n      \"responseHeaders\": [\r\n        {\r\n          \"id\": 0,\r\n          \"response\": \"string\",\r\n          \"headerKey\": \"string\",\r\n          \"headerValue\": \"string\",\r\n          \"dataType\": {\r\n            \"id\": 0,\r\n            \"dataTypeName\": \"string\"\r\n          }\r\n        }\r\n      ]\r\n    }\r\n  }\r\n]";
            var settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var routes = JsonConvert.DeserializeObject<List<Models.Claims.Route>>(content, settings);

            var viewModel = routes.Select(r => new RouteViewModel
            {
                Id = r.Id,
                RouteType = r.RouteType.RouteTypeName,
                RoutePath = r.RoutePath,
                Order = r.Order,
                RouteHeaders = r.RouteHeaders.Select(rh => new RouteHeaderViewModel
                {
                    HeaderKey = rh.HeaderKey,
                    HeaderValue = rh.HeaderValue,
                    DataTypeName = rh.DataType.DataTypeName
                }).ToList(),
                RouteParameters = r.RouteParameters.Select(rp => new RouteParameterViewModel
                {
                    ParameterKey = rp.ParameterKey,
                    ParameterValue = rp.ParameterValue,
                    DataTypeName = rp.DataType.DataTypeName
                }).ToList(),
                MethodType = r.MethodType.MethodTypeName,
                Clients = r.Clients.Select(c => c.ClientName).ToList()
            }).ToList();

            return View("~/Views/Claims/ClaimsList.cshtml", viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
