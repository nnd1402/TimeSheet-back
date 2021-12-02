using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal TimeSheetDbContext Context { get; set; }
        protected DbSet<TEntity> dbSet;

        public GenericRepository(TimeSheetDbContext context)
        {
               this.Context = context;
               this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
     
        public TEntity GetById<TId>(TId id)
        {
            return dbSet.Find(id);
        }
        public TEntity Insert(TEntity obj)
        {
            return dbSet.Add(obj);
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
        public IEnumerable<TEntity> Search(Func<TEntity, bool> predicate)
        {
            return dbSet.Where(predicate);
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
           return dbSet.AddRange(entities);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
