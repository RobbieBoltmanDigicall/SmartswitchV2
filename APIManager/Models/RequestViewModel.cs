﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;
namespace APIManager.Models
{
    public class RequestViewModel
    {
        public Route Route { get; set; }
        public ICollection<ClientViewModel> Clients { get; set; }
        public List<SelectListItem> RouteTypes { get; set; }
        public List<SelectListItem> MethodTypes { get; set; }
        public List<SelectListItem> DataTypes { get; set; }

    }
}
