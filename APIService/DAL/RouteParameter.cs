namespace ClaimsService.DAL
{
    public class RouteParameter
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public DataType DataType { get; set; }
    }
}
