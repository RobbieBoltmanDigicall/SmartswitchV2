namespace ClaimsService.DAL
{
    public class RouteHeader
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public DataType DataType { get; set; }
    }
}
