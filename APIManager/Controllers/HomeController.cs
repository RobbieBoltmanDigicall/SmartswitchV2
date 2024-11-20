using APIManager.Models;
using APIManager.Services.USSDs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUSSDService _ussdService;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IUSSDService ussdService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _ussdService = ussdService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _ussdService.ReadLogs(DateTime.Now.AddDays(-7), DateTime.Now);

            List<LogViewModel> result = logs.Select(l => new LogViewModel()
            {
                System = l.System,
                RequestURL = l.RequestURL,
                LogType = l.LogType,
                Message = l.Message,
                Payload = l.Payload,
                CreatedDateTime = l.CreatedDateTime,
                Failed = l.Failed
            }).ToList();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
