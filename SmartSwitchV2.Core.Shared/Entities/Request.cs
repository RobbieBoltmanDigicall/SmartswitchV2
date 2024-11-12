using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Entities
{
    public class Request
    {
        public int ClientId { get; set; }
        public string RouteName { get; set; }
        public string Payload { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
    }
}
