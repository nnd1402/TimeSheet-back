using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class TimeSheetEntryRepository : GenericRepository<TimeSheetEntry>, ITimeSheetEntryRepository
    {
        public TimeSheetEntryRepository(TimeSheetDbContext context) : base(context)
        {
        }
        public IEnumerable<TimeSheetEntry> GetByDate(DateTime date)
        {
           var result = dbSet.Where(e => e.Date == date).ToList();
           return result;
        }
    }
}
