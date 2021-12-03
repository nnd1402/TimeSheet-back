using System;
using System.Data.Entity;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //public UserRepository(TimeSheetDbContext context) : base(context)
        //{         
        //}
    }
}
