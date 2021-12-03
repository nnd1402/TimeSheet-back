using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class TimeSheetEntryRepository : GenericRepository<TimeSheetEntry>, ITimeSheetEntryRepository
    {
        //public TimeSheetEntryRepository(TimeSheetDbContext context) : base(context)
        //{
        //}
    }
}
