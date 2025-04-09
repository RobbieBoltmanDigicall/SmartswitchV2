using APIManager.Models;
using APIManager.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAPIService _apiService;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IAPIService apiService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _apiService.ReadLogs(DateTime.Now.AddDays(-7), DateTime.Now);
            DashboardViewModel result = new DashboardViewModel();
            List<LogViewModel> logViewModels = new List<LogViewModel>();

            if (logs != null)
            {
                logViewModels = logs.Select(l => new LogViewModel()
                {
                    System = l.System,
                    RequestURL = l.RequestURL,
                    LogType = l.LogType,
                    Message = l.Message,
                    Payload = l.Payload,
                    CreatedDateTime = l.CreatedDateTime,
                    Failed = l.Failed
                }).ToList();
            }

            //TODO: Get systems dynamically
            result.Systems = new List<SystemDashboardViewModel>() {
                new SystemDashboardViewModel()
                {
                    SystemName = "API",
                    AmountOfRequests = logViewModels.Where(l => l.Message == "Executing route").Count(),
                    FailedRequests = logViewModels.Where(l => l.Message == "URL Failed - retrying with failover").Count(),
                    MostCommonFailedUrls = logViewModels.Where(l => l.Message == "URL Failed - retrying with failover")
                    .GroupBy(l => l.RequestURL)
                    .Select(l => l.Key).ToList()
                }
            };

            result.Systems.ForEach(s => s.FailedRatio = (float)s.FailedRequests / s.AmountOfRequests);

            if (logs != null)
            {
                result.Systems[0].RequestsPerHour = logs
                .Where(log => log.CreatedDateTime >= DateTime.Now.AddDays(-7) && log.Message == "Executing route")
                .GroupBy(log => new
                {
                    log.CreatedDateTime.Year,
                    log.CreatedDateTime.Month,
                    log.CreatedDateTime.Day,
                    log.CreatedDateTime.Hour
                })
                .Select(group => new RequestPerHour
                {
                    RequestDateTimeInterval = new DateTime(group.Key.Year, group.Key.Month, group.Key.Day, group.Key.Hour, 0, 0),
                    Count = group.Count()
                })
                .OrderBy(result => result.RequestDateTimeInterval)
                .ToList();
            }
            result.LogViewModels = logViewModels;

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
