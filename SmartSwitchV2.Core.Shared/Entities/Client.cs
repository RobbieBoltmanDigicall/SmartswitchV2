namespace SmartSwitchV2.Core.Shared.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<Route> Routes { get; set; }
    }
}
