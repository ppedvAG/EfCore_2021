using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        EfContext _context = new EfContext();

        public IGameRepository GameRepository => new EfGameRepository(_context);

        public IRepository<Genre> GenreRepository => new EfRepository<Genre>(_context);

        public IRepository<Company> CompanyRepository => new EfRepository<Company>(_context);

        public int SaveAll()
        {
            return _context.SaveChanges();
        }
    }
}
