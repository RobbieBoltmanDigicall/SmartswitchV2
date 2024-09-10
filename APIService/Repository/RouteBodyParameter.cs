namespace ClaimsService.Repository
{
    public class RouteBodyParameter
    {
        public int Id { get; set; }
        public int RouteBodyId { get; set; }
        public string BodyKey { get; set; }
        public string BodyValue { get; set; }
        public int DataTypeId { get; set; }
    }
}
