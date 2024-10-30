using SmartSwitchV2.DataLayer.HTTPDefinitions;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;

namespace ClaimsService.Models.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly SmartSwitchDbContext _context;

        public ResponseRepository(SmartSwitchDbContext context)
        {
            _context = context;
        }
    }
}
