namespace ClaimsService.DAL
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public ICollection<Route> Routes { get; set; }
    }
}
