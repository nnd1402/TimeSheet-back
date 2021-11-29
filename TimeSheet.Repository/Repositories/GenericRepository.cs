using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal TimeSheetDbContext Context { get; set; }
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TimeSheetDbContext context)
        {
               this.Context = context;
               this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }
        public void Insert(TEntity obj)
        {
            dbSet.Add(obj);
        }
        public void Update(int id, TEntity obj)
        {
            var existingEntity = dbSet.Find(id);
            Context.Entry(existingEntity).CurrentValues.SetValues(obj);
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            TEntity existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
