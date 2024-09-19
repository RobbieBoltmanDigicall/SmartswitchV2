using ClaimsService.DAL;
using ClaimsService.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Route = ClaimsService.DAL.Route;

namespace ClaimsService.Models.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly SmartSwitchDbContext _context;

        public RouteRepository(SmartSwitchDbContext context)
        {
            _context = context;
        }

        public bool AddRoute(Route route)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public List<Route> GetAllRoutes()
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var result = _context.Routes
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.RouteBodyParameters)
                        .ThenInclude(rbp => rbp.DataType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.ApplicationType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.BodyType)
                .Include(r => r.RouteParameters)
                    .ThenInclude(rp => rp.DataType)
                .Include(r => r.RouteHeaders)
                    .ThenInclude(rh => rh.DataType)
                .Include(r => r.RouteType)
                .Include(r => r.MethodType)
                .Include(r => r.Clients)
                .ToList();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            return result;
        }

        public List<Route> GetAllRoutesForClient(int clientId)
        {
            //TODO: Investigate impact of supressing warning. 
            //Seems to be fine due to EF building a query and no exception thrown when nullable types are returned from SQL.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var result = _context.Routes
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.RouteBodyParameters)
                        .ThenInclude(rbp => rbp.DataType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.ApplicationType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.BodyType)
                .Include(r => r.RouteParameters)
                    .ThenInclude(rp => rp.DataType)
                .Include(r => r.RouteHeaders)
                    .ThenInclude(rh => rh.DataType)
                .Include(r => r.RouteType)
                .Include(r => r.MethodType)
                .Include(r => r.Clients)
                .Where(r => r.Clients.Any(c => c.ClientId == clientId))
                .ToList();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            return result;
        }

        public Route GetRouteModelByRouteId(int routeId)
        {
            //TODO: Investigate impact of supressing warning. 
            //Seems to be fine due to EF building a query and no exception thrown when nullable types are returned from SQL.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var result = _context.Routes
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.RouteBodyParameters)
                        .ThenInclude(rbp => rbp.DataType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.ApplicationType)
                .Include(r => r.RouteBody)
                    .ThenInclude(rb => rb.BodyType)
                .Include(rp => rp.RouteParameters)
                    .ThenInclude(rp => rp.DataType)
                .Include(r => r.RouteHeaders)
                    .ThenInclude(rh => rh.DataType)
                .Include(r => r.RouteType)
                .Include(r => r.MethodType)
                .Include(r => r.Clients)
                .FirstOrDefault(r => r.RouteId == routeId);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            if (result == null)
                //TODO: Log null route
                throw new Exception("No route found");
            return result;            
        }

        public bool UpdateRoute(Route route)
        {
            throw new NotImplementedException();
        }
    }
}
