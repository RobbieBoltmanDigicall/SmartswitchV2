namespace APIManager.Models.Claims
{
    public class Route
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
        public ICollection<Client> Clients { get; set; }
        public Response Response { get; set; }
    }

    public class DataType
    {
        public int DataTypeId { get; set; }
        public string DataTypeName { get; set; }
    }

    public class Response
    {
        public int ResponseId { get; set; }
        public Route Route { get; set; }
    }

    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public ICollection<Route> Routes { get; set; }
    }

    public class MethodType
    {
        public int MethodTypeId { get; set; }
        public string MethodTypeName { get; set; }
        public Route Route { get; set; }
    }

    public class RouteType
    {
        public int RouteTypeId { get; set; }
        public string RouteTypeName { get; set; }
        public Route Route { get; set; }
    }

    public class RouteHeader
    {
        public int RouteHeaderId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }

    public class RouteParameter
    {
        public int RouteParameterId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }

    public class RouteBody
    {
        public int RouteBodyId { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int? ApplicationTypeId { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public ICollection<RouteBodyParameter> RouteBodyParameters { get; set; }
    }

    public class ApplicationType
    {
        public int ApplicationTypeId { get; set; }
        public string ApplicationTypeName { get; set; }
    }

    public class RouteBodyParameter
    {
        public int RouteBodyParameterId { get; set; }
        public int RouteBodyId { get; set; }
        public RouteBody RouteBody { get; set; }
        public string BodyKey { get; set; }
        public string BodyValue { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
    }

    public class BodyType
    {
        public int BodyTypeId { get; set; }
        public string BodyTypeName { get; set; }
    }

}
