namespace ClaimsService.DAL
{
    public class RouteHeader
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public int DataTypeId { get; set; }
    }
}
