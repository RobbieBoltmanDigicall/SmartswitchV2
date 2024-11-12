using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Entities
{    
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
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
        //public string? FailOverURL { get; set; }
        //public int? RetryAttempts { get; set; }
    }
}
