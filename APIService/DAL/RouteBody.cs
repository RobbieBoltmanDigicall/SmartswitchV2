namespace ClaimsService.DAL
{
    public class RouteBody
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public BodyType BodyType { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public ICollection<RouteBodyParameter> RouteBodyParameters { get; set; }
    }
}
