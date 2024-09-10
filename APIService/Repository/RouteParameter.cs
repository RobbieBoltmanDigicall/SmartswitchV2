namespace ClaimsService.Repository
{
    public class RouteParameter
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public int DataTypeId { get; set; }
    }
}
