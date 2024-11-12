using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Entities
{
    public class RouteHeader
    {
        public int RouteHeaderId { get; set; }
        public int RouteId { get; set; }
        //public Route Route { get; set; }
        public string HeaderKey { get; set; }
        public string? HeaderValue { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }
}
