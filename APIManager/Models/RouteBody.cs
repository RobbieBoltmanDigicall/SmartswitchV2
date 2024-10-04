namespace APIManager.Models
{
    public class RouteBody
    {
        public int RouteBodyId { get; set; }
        public int RouteId { get; set; }
        public RequestViewModel Route { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int? ApplicationTypeId { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public ICollection<RouteBodyParameter> RouteBodyParameters { get; set; }
    }
}
