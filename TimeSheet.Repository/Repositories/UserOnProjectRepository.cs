using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class UserOnProjectRepository : GenericRepository<UserOnProject>, IUserOnProjectRepository
    {
        public UserOnProjectRepository(TimeSheetDbContext context) : base(context)
        {
        }
    }
}
