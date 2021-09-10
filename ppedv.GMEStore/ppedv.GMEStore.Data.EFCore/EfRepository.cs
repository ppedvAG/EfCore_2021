using Microsoft.EntityFrameworkCore;
using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System.Linq;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfRepository : IRepository
    {
        EfContext _context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            //if (typeof(T) == typeof(Company))
            //    _context.Companies.Add(entity as Company);

            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public int SaveAll()
        {
            return _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<Game> QueryGamesIncludingAll()
        {
            return Query<Game>().Include(x => x.Genres).Include(x => x.Developer).Include(x => x.Publisher);
        }
    }
}
