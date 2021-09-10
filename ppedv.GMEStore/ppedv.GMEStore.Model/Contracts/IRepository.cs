using System.Collections.Generic;
using System.Linq;

namespace ppedv.GMEStore.Model.Contracts
{

    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Query();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
