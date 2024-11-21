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
            DashboardViewModel result = new DashboardViewModel();
            List<LogViewModel> logViewModels = logs.Select(l => new LogViewModel()
            {
                System = l.System,
                RequestURL = l.RequestURL,
                LogType = l.LogType,
                Message = l.Message,
                Payload = l.Payload,
                CreatedDateTime = l.CreatedDateTime,
                Failed = l.Failed
            }).ToList();

            //TODO: Get systems dynamically
            result.Systems = new List<SystemDashboardViewModel>() {
                new SystemDashboardViewModel()
                {
                    SystemName = "USSD",
                    AmountOfRequests = logViewModels.Where(l => l.Message == "Executing route").Count(),
                    FailedRequests = logViewModels.Where(l => l.Message == "URL Failed - retrying with failover").Count(),
                    MostCommonFailedUrls = logViewModels.Where(l => l.Message == "URL Failed - retrying with failover")
                    .GroupBy(l => l.RequestURL)
                    .Select(l => l.Key).ToList()
                }
            };

            result.Systems.ForEach(s => s.FailedRatio = (float)s.FailedRequests / s.AmountOfRequests);
            result.LogViewModels = logViewModels;

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
