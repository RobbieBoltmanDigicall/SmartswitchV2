using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.DataLayer.HTTPDefinitions
{
    public class ResponseMapping
    {
        public int ResponseMappingId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string ArgumentName { get; set; }
        public string ResponseArgumentName { get; set; }
        public int RequestComponentId { get; set; }
        public RequestComponent RequestComponent { get; set; }
    }
}
