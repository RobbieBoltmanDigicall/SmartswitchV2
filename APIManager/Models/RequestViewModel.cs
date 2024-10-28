﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace APIManager.Models
{
    public class RequestViewModel
    {
        public int RouteId { get; set; }
        public int RouteTypeId { get; set; }
        public RouteType RouteType { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public ICollection<RouteHeader>? RouteHeaders { get; set; }
        public ICollection<RouteParameter>? RouteParameters { get; set; }
        public RouteBody? RouteBody { get; set; }
        public int MethodTypeId { get; set; }
        public MethodType MethodType { get; set; }
        public ICollection<ClientViewModel> Clients { get; set; }
        public Response Response { get; set; }
        public List<SelectListItem> RouteTypes { get; set; }
        public List<SelectListItem> MethodTypes { get; set; }
        public List<SelectListItem> DataTypes { get; set; }
        public string? FailOverURL { get; set; }
        public int? RetryAttempts { get; set; }
    }
}
