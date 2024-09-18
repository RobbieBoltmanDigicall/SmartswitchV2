using ClaimsService.Models;
using ClaimsService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIService.Controllers
{
    [Route("Claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IRouteService _routeService;

        public ClaimsController(ILogger<ClaimsController> logger, IRouteService routeService)
        {
            _logger = logger;
            _routeService = routeService;
        }


        [HttpPost("CreateClaim")]
        public async Task<ResponseModel> CreateClaim(RequestModel requestDetail)
        {
            return new ResponseModel { };
        }
    }
}
