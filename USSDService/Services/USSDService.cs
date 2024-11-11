using ClaimsService.Models;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using System.Text;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;
using SmartSwitchV2.Core.Shared.Entities;

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
            var routeToUpdate = new Route()
            {
                RouteId = route.RouteId,
                RouteTypeId = route.RouteType.RouteTypeId,
                SystemId = 2,
                //RouteType = new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteType()
                //{
                    
                //    RouteTypeId = route.RouteType.RouteTypeId,
                //    RouteTypeName = route.RouteType.RouteTypeName
                //},
                RoutePath = route.RoutePath,
                Order = route.Order,
                MethodTypeId = route.MethodType.MethodTypeId,
                //MethodType = new SmartSwitchV2.DataLayer.HTTPDefinitions.MethodType()
                //{
                //    MethodTypeId = route.MethodType.MethodTypeId,
                //    MethodTypeName = route.MethodType.MethodTypeName
                //},
                Clients = route.Clients?.Select(c => new SmartSwitchV2.DataLayer.HTTPDefinitions.Client()
                {
                    ClientId = c.ClientId,
                    ClientName = c.ClientName
                }).ToList(),
                //Response = route.Response,
                //FailOverURL = route.FailOverURL,
                //RetryAttempts = route.RetryAttempts
            };

            if (route.RouteHeaders != null)
            {
                routeToUpdate.RouteHeaders = route.RouteHeaders.Select(rh => new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteHeader()
                {
                    RouteHeaderId = rh.RouteHeaderId,
                    RouteId = rh.RouteId,
                    HeaderKey = rh.HeaderKey,
                    HeaderValue = rh.HeaderValue,
                    DataTypeId = rh.DataTypeId,
                    //DataType = new SmartSwitchV2.DataLayer.HTTPDefinitions.DataType()
                    //{
                    //    DataTypeId = rh.DataType.DataTypeId,
                    //    DataTypeName = rh.DataType.DataTypeName
                    //}
                }).ToList();
            }

            if (route.RouteParameters != null)
            {
                routeToUpdate.RouteParameters = route.RouteParameters.Select(rp => new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteParameter()
                {
                    RouteParameterId = rp.RouteParameterId,
                    RouteId = rp.RouteId,
                    ParameterKey = rp.ParameterKey,
                    ParameterValue = rp.ParameterValue,
                    //DataTypeId = rp.DataTypeId,
                    DataType = new SmartSwitchV2.DataLayer.HTTPDefinitions.DataType()
                    {
                        DataTypeId = rp.DataType.DataTypeId,
                        DataTypeName = rp.DataType.DataTypeName
                    }
                }).ToList();
            }

            if (route.RouteBody != null)
            {
                routeToUpdate.RouteBody = new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteBody()
                {
                    RouteBodyId = route.RouteBody.RouteBodyId,
                    RouteId = route.RouteBody.RouteId,
                    //BodyTypeId = route.RouteBody.BodyTypeId,
                    BodyType = new SmartSwitchV2.DataLayer.HTTPDefinitions.BodyType()
                    {
                        BodyTypeId = route.RouteBody.BodyType.BodyTypeId,
                        BodyTypeName = route.RouteBody.BodyType.BodyTypeName
                    },
                    ApplicationTypeId = route.RouteBody.ApplicationTypeId,
                    //ApplicationType = route.RouteBody.ApplicationTypeId.HasValue ? new SmartSwitchV2.DataLayer.HTTPDefinitions.ApplicationType()
                    //{
                    //    ApplicationTypeId = route.RouteBody.ApplicationType.ApplicationTypeId,
                    //    ApplicationTypeName = route.RouteBody.ApplicationType.ApplicationTypeName
                    //} : null,
                    RouteBodyParameters = route.RouteBody.RouteBodyParameters.Select(rbp => new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteBodyParameter()
                    {
                        RouteBodyParameterId = rbp.RouteBodyParameterId,
                        RouteBodyId = rbp.RouteBodyId,
                        BodyKey = rbp.BodyKey,
                        BodyValue = rbp.BodyValue,
                        DataTypeId = rbp.DataTypeId
                        //DataType = new SmartSwitchV2.DataLayer.HTTPDefinitions.DataType()
                        //{
                        //    DataTypeId = rbp.DataType.DataTypeId,
                        //    DataTypeName = rbp.DataType.DataTypeName
                        //}
                    }).ToList()
                };
            }

            return _routeRepository.UpdateRoute(routeToUpdate);
        }


        public async Task<bool> CallRoute(Request request)
        {
            //var _httpClient = new HttpClient();

            //var jsonPayload = JsonConvert.SerializeObject(request.ClaimsBody);
            //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            //// TODO: Add logging

            //// TODO: Get client's Claim URL here
            //var claimUrl = "";

            //var response = await _httpClient.PostAsync(claimUrl, content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    // TODO: log failure in error table
            //    throw new Exception($"Failed to send claim payload. Status code: {response.StatusCode}");
            //}
            return true;
        }
    }
}
