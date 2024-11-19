using SmartSwitchV2.DataLayer.HTTPDefinitions;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Route = SmartSwitchV2.DataLayer.HTTPDefinitions.Route;

namespace ClaimsService.Models.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly SmartSwitchDbContext _context;

        public RouteRepository(SmartSwitchDbContext context)
        {
            _context = context;
        }

        public bool DeleteRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public List<Route> GetAllRoutes(int systemId, bool lazyLoad = true)
        {
            if (!lazyLoad)
            {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                var result = _context.Routes.Where(r => r.SystemId == systemId)
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
            else
            {
                var result = _context.Routes.Where(r => r.SystemId == systemId)
                    .Include(r => r.RouteBody)
                    .Include(r => r.RouteType)
                    .Include(r => r.MethodType)
                    .Include(r => r.Clients)
                    .ToList();

                return result;
            }
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

        public Route GetRouteModelByRouteName(string name)
        {
            try
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
                    .FirstOrDefault(r => r.RouteName == name);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

                if (result == null)
                    //TODO: Log null route
                    throw new Exception("No route found");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Route GetRouteModelByRouteId(int routeId)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
                      
        }

        public bool InsertUpdateRoute(Route route)
        {
            try
            {
                if (route.RouteId != 0)
                {
                    //Remove children objects that are not included in updated route argument.
                    var headersToDelete = new List<RouteHeader>();
                    var parametersToDelete = new List<RouteParameter>();
                    var bodyParametersToDelete = new List<RouteBodyParameter>();
                    var routeBody = _context.RouteBodies.AsNoTracking().Where(rb => rb.RouteId == route.RouteId).FirstOrDefault() ?? route.RouteBody;
                    var headers = _context.RouteHeaders.AsNoTracking().Where(h => h.RouteId == route.RouteId).ToList();
                    var parameters = _context.RouteParameters.AsNoTracking().Where(p => p.RouteId == route.RouteId).ToList();
                    var bodyParameters = routeBody == null ? null
                        : _context.RouteBodyParameters.AsNoTracking().Where(bp => bp.RouteBodyId == routeBody.RouteBodyId).ToList();


                    if (headers?.Count > 0)
                    {
                        if (route.RouteHeaders?.Count > 0)
                        {
                            headersToDelete = headers.Where(h => !route.RouteHeaders.Any(rh => rh.RouteHeaderId == h.RouteHeaderId)).ToList();
                        }
                        else
                            headersToDelete = headers.ToList();
                        _context.RouteHeaders.RemoveRange(headersToDelete);
                    }

                    if (parameters?.Count > 0)
                    {
                        if (route.RouteParameters?.Count > 0)
                        {
                            parametersToDelete = parameters.Where(p => !route.RouteParameters.Any(rp => rp.RouteParameterId == p.RouteParameterId)).ToList();
                        }
                        else
                            parametersToDelete = parameters.ToList();
                        _context.RouteParameters.RemoveRange(parametersToDelete);
                    }


                    if (route.RouteBody?.RouteBodyParameters.Count > 0)
                    {
                        bodyParametersToDelete = bodyParameters?.Where(bp => !route.RouteBody.RouteBodyParameters.Any(rbp => rbp.RouteBodyParameterId == bp.RouteBodyParameterId)).ToList();
                    }
                    else
                        bodyParametersToDelete = bodyParameters?.ToList();
                    if (bodyParametersToDelete != null)
                        _context.RouteBodyParameters.RemoveRange(bodyParametersToDelete);
                }

                _context.Routes.Update(route);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
