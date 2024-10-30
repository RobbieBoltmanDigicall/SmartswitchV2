namespace USSDService.Services
{
    public interface IUSSDService
    {
        List<Route> GetAllUSSDs(bool lazyLoad = true);
        Route GetUSSDById(int ussdId);
    }
}
