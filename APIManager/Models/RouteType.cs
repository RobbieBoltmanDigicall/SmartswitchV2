﻿namespace APIManager.Models
{
    public class RouteType
    {
        public int RouteTypeId { get; set; }
        public string RouteTypeName { get; set; }
        public RequestViewModel Route { get; set; }
    }
}