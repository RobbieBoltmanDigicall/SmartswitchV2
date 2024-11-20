namespace SmartSwitchV2.DataLayer.HTTPDefinitions
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public int SystemId { get; set; }
        public int RouteTypeId { get; set; }
        public RouteType RouteType { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public List<RouteHeader>? RouteHeaders { get; set; }
        public List<RouteParameter>? RouteParameters { get; set; }
        public RouteBody? RouteBody { get; set; }
        public int MethodTypeId { get; set; }
        public MethodType MethodType { get; set; }
        public List<Client>? Clients { get; set; }
        public Response Response { get; set; }
        public int? RouteParentId { get; set; }
        public Route? RouteParent { get; set; }
        public string? FailOverURL { get; set; }
        public int? RetryAttempts { get; set; }
    }
}
