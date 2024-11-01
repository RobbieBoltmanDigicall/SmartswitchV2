﻿namespace SmartSwitchV2.DataLayer.HTTPDefinitions
{
    public class Route
    {
        public int RouteId { get; set; }
        public int SystemId { get; set; }
        public int RouteTypeId { get; set; }
        public RouteType RouteType { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public ICollection<RouteHeader>? RouteHeaders { get; set; }
        public ICollection<RouteParameter>? RouteParameters { get; set; }
        public RouteBody? RouteBody { get; set; }
        public int MethodTypeId { get; set; }
        public MethodType MethodType { get; set; }
        public ICollection<Client> Clients { get; set; }
        public Response Response { get; set; }
    }
}
