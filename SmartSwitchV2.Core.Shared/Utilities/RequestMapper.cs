using Newtonsoft.Json;
using SmartSwitchV2.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace SmartSwitchV2.Core.Shared.Utilities
{
    public static class RequestMapper
    {
        public static HttpRequestMessage MapRouteToHTTPRequest(Route route, Request processRequest)
        {
            HttpMethod httpMethod;

            switch (route.MethodType.MethodTypeName)
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "PATCH":
                    httpMethod = HttpMethod.Patch;
                    break;
                case "HEAD":
                    httpMethod = HttpMethod.Head;
                    break;
                case "OPTIONS":
                    httpMethod = HttpMethod.Options;
                    break;
                default:
                    httpMethod = HttpMethod.Get;
                    break;
            }            

            if (route.RouteParameters != null && route.RouteParameters.Count > 0)
            {
                route.RoutePath += "?";
                foreach (var param in route.RouteParameters)
                {
                    var parameterPair = processRequest.Parameters?.FirstOrDefault(p => p.Key == param.ParameterKey);
                    if (parameterPair == null)
                        throw new Exception("Request parameters does not match route parameters");
                    route.RoutePath += $"{param.ParameterKey}={parameterPair.Value.Value ?? param.ParameterValue}&";
                }
                // This removes the trailing separating char '&'
                route.RoutePath = route.RoutePath.Remove(route.RoutePath.Length - 1);
            }

            var request = new HttpRequestMessage(httpMethod, route.RoutePath);

            if (route.RouteHeaders != null && route.RouteHeaders.Count > 0)
                foreach (var header in route.RouteHeaders)
                {
                    var headerPair = processRequest.Headers?.FirstOrDefault(h => h.Key == header.HeaderKey);
                    if (headerPair == null)
                        throw new Exception("Request headers does not match route headers");
                    if (!String.IsNullOrEmpty(header.HeaderValue) || !String.IsNullOrEmpty(headerPair.Value.Value))
                        request.Headers.Add(header.HeaderKey, $"{(!String.IsNullOrEmpty(header.HeaderValue) ? header.HeaderValue + headerPair.Value.Value : headerPair.Value.Value) }");
                }
                    
            
            Dictionary<string, string> bodyParamsPairs = new();

            if (route.RouteBody != null && route.RouteBody.RouteBodyParameters.Count > 0)
            {
                var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(processRequest.Payload);
                foreach (var param in route.RouteBody.RouteBodyParameters)
                {
                    var bodyParamPair = requestBody?.FirstOrDefault(p => p.Key == param.BodyKey);
                    if (bodyParamPair == null)
                        throw new Exception("Request body parameters does not match route body parameters");
                    bodyParamsPairs.Add(param.BodyKey, bodyParamPair.Value.Value ?? param.BodyValue);
                }

                var bodyParamsString = JsonConvert.SerializeObject(bodyParamsPairs);
                request.Content = new StringContent(bodyParamsString, null, $"Application/{route.RouteBody.ApplicationType.ApplicationTypeName}");
            }

            return request;
        }
    }
}
