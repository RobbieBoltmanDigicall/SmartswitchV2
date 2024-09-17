namespace ClaimsService.Models
{
    public class RequestModel
    {
        public int ClientId { get; set; }
        public string Payload { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
    }
}
