using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Entities
{
    public class Response
    {
        public int ResponseId { get; set; }
        public string ResponseContent { get; set; } = string.Empty;
        public HttpStatusCode ResponseStatus { get; set; } = HttpStatusCode.NoContent;
    }
}
