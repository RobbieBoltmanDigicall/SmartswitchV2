﻿using SmartSwitchV2.Core.Shared.Enums;

namespace SmartSwitchV2.Core.Shared.Entities 
{
    public class Log
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string RequestURL { get; set; }
        public LogType LogType { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool Failed { get; set; }
    }
}
