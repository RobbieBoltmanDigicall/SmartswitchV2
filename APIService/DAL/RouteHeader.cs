namespace ClaimsService.DAL
{
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
}
