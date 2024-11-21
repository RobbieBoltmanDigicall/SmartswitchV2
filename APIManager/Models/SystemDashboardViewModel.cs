namespace APIManager.Models
{
    public class SystemDashboardViewModel
    {
        public string SystemName { get; set; }
        public int AmountOfRequests { get; set; }
        public int FailedRequests { get; set; }
        public List<string> MostCommonFailedUrls { get; set; }
        public float FailedRatio { get; set; }
        public List<RequestPerHour> RequestsPerHour { get; set; } = new();
    }
}
