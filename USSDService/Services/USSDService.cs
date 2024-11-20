using ClaimsService.Models;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using System.Text;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;
using SmartSwitchV2.Core.Shared.Entities;
using SmartSwitchV2.Core.Shared.Utilities;
using Newtonsoft.Json;

namespace USSDService.Services
{
    public class USSDService : IUSSDService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IResponseRepository _responseRepository;

        public USSDService(IRouteRepository routeRepository, IResponseRepository responseRepository)
        {
            _routeRepository = routeRepository;
            _responseRepository = responseRepository;
        }

        public List<Route> GetAllUSSDRoutes(bool lazyLoad) =>        
            _routeRepository.GetAllRoutes(2, lazyLoad);
        

        public Route GetUSSDRouteById(int routeId) =>
            _routeRepository.GetRouteModelByRouteId(routeId);

        public bool UpdateUSSDRoute(SmartSwitchV2.Core.Shared.Entities.Route route)
        {
            var routeToUpdate = ClassConverter.RouteConvert(route);

            return _routeRepository.InsertUpdateRoute(routeToUpdate);
        }

        public Route GetUSSDRouteByName(string name)
        => _routeRepository.GetRouteModelByRouteName(name);        

        public async Task<Response> ProcessUSSDRequest(Request request)
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

        private async Task<Response> ExecuteRequest(Route route, Request request)
        {
            var httpRequest = RequestMapper.MapRouteToHTTPRequest(route, request);
            HttpClient httpClient = new();
            var response = httpClient.Send(httpRequest);

            return new Response()
            {
                ResponseContent = await response.Content.ReadAsStringAsync(),
                ReasonPhrase = response.ReasonPhrase,
                ResponseStatus = response.StatusCode
            };
        }
    }
}
