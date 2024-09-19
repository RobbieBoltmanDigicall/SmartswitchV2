namespace ClaimsService.DAL
{
    public class RouteType
    {
        public int RouteTypeId { get; set; }
        public string RouteTypeName { get; set; }
        public Route Route { get; set; }
    }
}
