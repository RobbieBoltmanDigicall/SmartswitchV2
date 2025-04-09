using ClaimsService.Models;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using System.Text;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;
using SmartSwitchV2.Core.Shared.Entities;
using SmartSwitchV2.Core.Shared.Utilities;
using Newtonsoft.Json;
using SmartSwitchV2.Core.Shared.Enums;

namespace APIService.Services
{
    public class APIService : IAPIService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IResponseRepository _responseRepository;

        public APIService(IRouteRepository routeRepository, IResponseRepository responseRepository)
        {
            _routeRepository = routeRepository;
            _responseRepository = responseRepository;
        }

        //TODO: Get SystemId from appsettings
        public List<Route> GetAllAPIRoutes(bool lazyLoad) =>        
            _routeRepository.GetAllRoutes(2, lazyLoad);
        

        public Route GetAPIRouteById(int routeId) =>
            _routeRepository.GetRouteModelByRouteId(routeId);

        public bool UpdateAPIRoute(SmartSwitchV2.Core.Shared.Entities.Route route)
        {
            var routeToUpdate = ClassConverter.RouteConvert(route);

            return _routeRepository.InsertUpdateRoute(routeToUpdate);
        }

        public Route GetAPIRouteByName(string name)
        => _routeRepository.GetRouteModelByRouteName(name);        

        public async Task<Response> ProcessAPIRequest(Request request)
        {
            var route = _routeRepository.GetRouteModelByRouteName(request.RouteName);
            var linkedRoutes = _routeRepository.GetLinkedRoutes(route.RouteId);
            linkedRoutes.Add(route);
            linkedRoutes = linkedRoutes.OrderBy(r => r.Order).ToList();
            var responseMappings = _responseRepository.ListResponseMappingsByRouteId(route.RouteId);
            Response responseObject = new();

            foreach (var lr in linkedRoutes)
            {
                if (lr.RouteType.RouteTypeName == "REST")
                {
                    responseObject = await ExecuteRequest(lr, request);

                    // Perform FailOverURL and retry attempts - start at 1 due to already having executed request once at this point
                    if (responseObject.ResponseStatus != System.Net.HttpStatusCode.OK
                    && lr.RetryAttempts.HasValue
                    && lr.RetryAttempts.Value > 0
                    && !String.IsNullOrEmpty(lr.FailOverURL))
                    {
                        _routeRepository.InsertLog(LogType.Warning.ToString(), "API", lr.RoutePath, "URL Failed - retrying with failover", JsonConvert.SerializeObject(lr, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore}), failed: true);
                        for (int i = 1; i < lr.RetryAttempts; i++)
                        {
                            lr.RoutePath = lr.FailOverURL;
                            responseObject = await ExecuteRequest(lr, request);

                            if (responseObject.ResponseStatus == System.Net.HttpStatusCode.OK)
                                break;
                        }
                    }

                    if (responseObject.ResponseStatus == System.Net.HttpStatusCode.OK && lr.RouteParentId.HasValue)
                    {
                        var content = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseObject.ResponseContent);
                        if (content != null)
                        {
                            foreach (var item in content)
                            {
                                var mapping = responseMappings.FirstOrDefault(rm => rm.ResponseArgumentName == item.Key);

                                if (mapping != null)
                                    switch (mapping.RequestComponentId)
                                    {
                                        case 1: //Headers
                                            if (request.Headers != null)
                                                request.Headers[mapping.ArgumentName] = item.Value;
                                            break;
                                        case 2: //Parameters
                                            if (request.Parameters != null)
                                                request.Parameters[mapping.ArgumentName] = item.Value;
                                            break;
                                        case 3: //BodyParameters
                                            break;
                                    }
                            }
                        }
                    }
                }
            }           

            return responseObject;
        }

        public async Task<List<Log>> ReadLogs(DateTime startDate, DateTime endDate)
        {
            var logs = _routeRepository.ReadLogs(startDate, endDate);

            List<Log> logsResult = logs.Select(l => new Log()
            {
                Id = l.Id,
                System = l.System,
                RequestURL = l.RequestURL,
                LogType = Enum.TryParse("Active", out LogType logType) ? logType : default,
                Message = l.Message,
                Payload = l.Payload,
                StackTrace = l.StackTrace,
                CreatedDateTime = l.CreatedDateTime,
                Failed = l.Failed
            }).ToList();

            return logsResult;
        }

        private async Task<Response> ExecuteRequest(Route route, Request request)
        {
            var httpRequest = RequestMapper.MapRouteToHTTPRequest(route, request);
            HttpClient httpClient = new();
            _routeRepository.InsertLog(LogType.Info.ToString(), "API", route.RoutePath, "Executing route", JsonConvert.SerializeObject(route, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), failed: false);
            var response = httpClient.Send(httpRequest);

            var responseObject = new Response()
            {
                ResponseContent = await response.Content.ReadAsStringAsync(),
                ReasonPhrase = response.ReasonPhrase,
                ResponseStatus = response.StatusCode
            };

            _routeRepository.InsertLog(LogType.Info.ToString(), "API", route.RoutePath, "Request response", $"Status Code: {responseObject.ResponseStatus}; Reasone Phrase: {(responseObject.ReasonPhrase != null ? responseObject.ReasonPhrase : "N/A")};", failed: responseObject.ResponseStatus != System.Net.HttpStatusCode.OK);

            return responseObject;
        }     
    }
}
