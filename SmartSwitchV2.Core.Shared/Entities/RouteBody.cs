using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Entities
{
    public class RouteBody
    {
        public int RouteBodyId { get; set; }
        public int RouteId { get; set; }
        //public Route Route { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int? ApplicationTypeId { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public List<RouteBodyParameter> RouteBodyParameters { get; set; }
    }
}
