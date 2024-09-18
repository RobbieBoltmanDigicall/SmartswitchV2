namespace ClaimsService.DAL
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public List<Route> Routes { get; set; }
    }
}
