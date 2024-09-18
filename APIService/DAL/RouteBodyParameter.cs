namespace ClaimsService.DAL
{
    public class RouteBodyParameter
    {
        public int Id { get; set; }
        public RouteBody RouteBody { get; set; }
        public string BodyKey { get; set; }
        public string BodyValue { get; set; }
        public DataType DataType { get; set; }
    }
}
