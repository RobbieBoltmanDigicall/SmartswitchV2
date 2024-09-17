using ClaimsService.Enums;
using ClaimsService.Repository;

namespace ClaimsService.Models
{
    public class RouteModel
    {
        public int Id { get; set; }
        public int RouteTypeId { get; set; }
        public string RoutePath { get; set; } = null!;
        public int Order { get; set; }
        public RouteBodyModel? RouteBody { get; set; }
        public List<Client> Clients { get; set; } = [];
        public List<RouteHeader>? RouteHeaders { get; set; }
        public List<RouteParameter>? Parameters { get; set; }
        public BodyTypes BodyType { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public RouteTypes RouteType { get; set; }
    }

    public class RouteBodyModel()
    {
        public List<RouteBodyParameter> Parameters { get; set; } = [];
    }
}
