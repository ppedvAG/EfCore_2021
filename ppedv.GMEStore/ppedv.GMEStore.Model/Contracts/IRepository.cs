using System.Collections.Generic;
using System.Linq;

namespace ppedv.GMEStore.Model.Contracts
{
    public interface IRepository
    {

        IQueryable<Game> QueryGamesIncludingAll();

        IQueryable<T> Query<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        void Add<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;

        int SaveAll();
    }
}
