namespace APIManager.Models.Claims
{
    public class Route
    {
        public int Id { get; set; }
        public RouteType RouteType { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public List<RouteHeader> RouteHeaders { get; set; }
        public List<RouteParameter> RouteParameters { get; set; }
        public MethodType MethodType { get; set; }
        public List<Client> Clients { get; set; }
    }

    public class RouteType
    {
        public int Id { get; set; }
        public string RouteTypeName { get; set; }
    }

    public class RouteHeader
    {
        public int Id { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public DataType DataType { get; set; }
    }

    public class RouteParameter
    {
        public int Id { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public DataType DataType { get; set; }
    }

    public class MethodType
    {
        public int Id { get; set; }
        public string MethodTypeName { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
    }

    public class DataType
    {
        public int Id { get; set; }
        public string DataTypeName { get; set; }
    }

    public class RouteViewModel
    {
        public int Id { get; set; }
        public string RouteType { get; set; }
        public string RoutePath { get; set; }
        public int Order { get; set; }
        public List<RouteHeaderViewModel> RouteHeaders { get; set; }
        public List<RouteParameterViewModel> RouteParameters { get; set; }
        public string MethodType { get; set; }
        public List<string> Clients { get; set; }
    }

    public class RouteHeaderViewModel
    {
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public string DataTypeName { get; set; }
    }

    public class RouteParameterViewModel
    {
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public string DataTypeName { get; set; }
    }
}
