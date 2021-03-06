using System;
using System.Collections.Generic;

namespace TimeSheet.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById<TId>(TId id1, TId id2);
        TEntity GetById<TId>(TId id);
        //TEntity GetByIdList<TId>(IEnumerable<TId> idList);
        TEntity Insert(TEntity obj);
        void Update(int id, TEntity obj);
        void Delete(int obj);
        void Delete (int id1, int id2);
        void Save();
        IEnumerable<TEntity> Search(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
