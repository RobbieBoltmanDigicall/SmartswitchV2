using SmartSwitchV2.DataLayer.HTTPDefinitions;

namespace SmartSwitchV2.DataLayer.Repositories.Interfaces
{
    public interface IResponseRepository
    {
        List<ResponseMapping> ListResponseMappingsByRouteId(int routeId);
    }
}
