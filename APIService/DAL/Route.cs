namespace ClaimsService.DAL
{
    public class Route
    {
        public int Id { get; set; }
        public int RouteTypeId { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public List<Client> Clients { get; set; }
    }
}
