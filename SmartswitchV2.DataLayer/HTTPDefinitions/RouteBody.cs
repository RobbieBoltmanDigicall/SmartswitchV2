namespace SmartSwitchV2.DataLayer.HTTPDefinitions
{
    public class RouteBody
    {
        public int RouteBodyId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int? ApplicationTypeId { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public List<RouteBodyParameter> RouteBodyParameters { get; set; }
    }
}
