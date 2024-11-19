using SmartSwitchV2.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitchV2.Core.Shared.Utilities
{
    public static class ClassConverter
    {
        public static DataLayer.HTTPDefinitions.Route RouteConvert(Route route)
        {
            var routeConverted = new DataLayer.HTTPDefinitions.Route()
            {
                RouteId = route.RouteId,
                RouteTypeId = route.RouteType.RouteTypeId,
                RouteName = route.RouteName,
                SystemId = 2,
                RoutePath = route.RoutePath,
                Order = route.Order,
                MethodTypeId = route.MethodType.MethodTypeId,
                Clients = route.Clients?.Select(c => new SmartSwitchV2.DataLayer.HTTPDefinitions.Client()
                {
                    ClientId = c.ClientId,
                    ClientName = c.ClientName
                }).ToList(),
                RouteParentId = route.RouteParentId
                //Response = route.Response,
                //FailOverURL = route.FailOverURL,
                //RetryAttempts = route.RetryAttempts
            };

            if (route.RouteHeaders != null)
            {
                routeConverted.RouteHeaders = route.RouteHeaders.Select(rh => new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteHeader()
                {
                    RouteHeaderId = rh.RouteHeaderId,
                    RouteId = rh.RouteId,
                    HeaderKey = rh.HeaderKey,
                    HeaderValue = rh.HeaderValue,
                    DataTypeId = rh.DataTypeId,
                }).ToList();
            }

            if (route.RouteParameters != null)
            {
                routeConverted.RouteParameters = route.RouteParameters.Select(rp => new SmartSwitchV2.DataLayer.HTTPDefinitions.RouteParameter()
                {
                    RouteParameterId = rp.RouteParameterId,
                    RouteId = rp.RouteId,
                    ParameterKey = rp.ParameterKey,
                    ParameterValue = rp.ParameterValue,
                    DataTypeId = rp.DataTypeId,
                }).ToList();
            }

            if (route.RouteBody != null)
            {
                routeConverted.RouteBody = new DataLayer.HTTPDefinitions.RouteBody()
                {
                    RouteBodyId = route.RouteBody.RouteBodyId,
                    RouteId = route.RouteBody.RouteId,
                    BodyType = new DataLayer.HTTPDefinitions.BodyType()
                    {
                        BodyTypeId = route.RouteBody.BodyType.BodyTypeId,
                        BodyTypeName = route.RouteBody.BodyType.BodyTypeName
                    },
                    ApplicationTypeId = route.RouteBody.ApplicationType.ApplicationTypeId,
                    ApplicationType = new DataLayer.HTTPDefinitions.ApplicationType()
                    {
                        ApplicationTypeId = route.RouteBody.ApplicationType.ApplicationTypeId,
                        ApplicationTypeName = route.RouteBody.ApplicationType.ApplicationTypeName
                    },
                    RouteBodyParameters = route.RouteBody.RouteBodyParameters.Select(rbp => new DataLayer.HTTPDefinitions.RouteBodyParameter()
                    {
                        RouteBodyParameterId = rbp.RouteBodyParameterId,
                        RouteBodyId = rbp.RouteBodyId,
                        BodyKey = rbp.BodyKey,
                        BodyValue = rbp.BodyValue,
                        DataTypeId = rbp.DataTypeId
                    }).ToList()
                };
            }

            return routeConverted;
        }
    }
}
