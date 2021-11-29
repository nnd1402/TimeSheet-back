using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(TimeSheetDbContext context) : base(context)
        {
            
        }
    }
}
