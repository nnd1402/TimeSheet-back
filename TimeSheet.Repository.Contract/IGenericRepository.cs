using System.Collections.Generic;

namespace TimeSheet.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Insert(TEntity obj);
        void Update(int id, TEntity obj);
        void Delete(int obj);
    }
}
