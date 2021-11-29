using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(TimeSheetDbContext context) : base(context)
        {
        }
    }
}
