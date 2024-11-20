﻿using APIManager.Models;
using SmartSwitchV2.Core.Shared.Entities;
using Route = SmartSwitchV2.Core.Shared.Entities.Route;

namespace APIManager.Services.USSDs
{
    public interface IUSSDService
    {
        Task<List<RequestViewModel>> ListAllUSSDRoutes(bool lazyLoad = true);
        Task<RequestViewModel> GetUSSDRouteById(int ussdRouteId);
        Task<bool> UpdateRequest(Route route);
        Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate);
    }
}
