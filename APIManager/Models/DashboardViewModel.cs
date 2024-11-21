using SmartSwitchV2.Core.Shared.Enums;

namespace APIManager.Models
{
    public class DashboardViewModel
    {
        public List<LogViewModel> LogViewModels { get; set; } = new();
        public List<SystemDashboardViewModel> Systems { get; set; } = new();
    }
}
