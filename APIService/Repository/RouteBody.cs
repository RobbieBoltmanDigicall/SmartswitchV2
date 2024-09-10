namespace ClaimsService.Repository
{
    public class RouteBody
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int BodyTypeId { get; set; }
        public int? ApplicationTypeId { get; set; }
    }
}
