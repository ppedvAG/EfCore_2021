using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System.Linq;

namespace ppedv.GMEStore.Data.EFCore
{

    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly EfContext _context;

        public EfRepository(EfContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            //if (typeof(T) == typeof(Company))
            //    _context.Companies.Add(entity as Company);

            _context.Set<T>().Add(entity);
        }


        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
