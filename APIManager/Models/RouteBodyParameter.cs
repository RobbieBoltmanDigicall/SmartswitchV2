namespace APIManager.Models
{
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
}
