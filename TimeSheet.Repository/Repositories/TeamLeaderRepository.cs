using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class TeamLeaderRepository : GenericRepository<TeamLeader>, ITeamLeaderRepository
    {
        public TeamLeaderRepository(TimeSheetDbContext context) : base(context)
        {
        }
    }
}
