using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.DataLayer.HTTPDefinitions
{
    public class Log
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string RequestURL { get; set; }
        public string LogType { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool Failed { get; set; }
    }
}
