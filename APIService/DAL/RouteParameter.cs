namespace ClaimsService.DAL
{
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
}
