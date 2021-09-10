using Microsoft.EntityFrameworkCore;
using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using ppedv.GMEStore.Model.Exceptions;
using System;
using System.Linq;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        EfContext _context = new EfContext();

        public IGameRepository GameRepository => new EfGameRepository(_context);

        public IRepository<Genre> GenreRepository => new EfRepository<Genre>(_context);

        public IRepository<Company> CompanyRepository => new EfRepository<Company>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }


        public void ConcurrencyUserWins(Entity entity)
        {
            var entry = _context.ChangeTracker.Entries().First(x => x.Entity == entity);
            entry.OriginalValues.SetValues(entry.GetDatabaseValues());

        }

        public void ConcurrencyDBWins(Entity entity)
        {
            _context.ChangeTracker.Entries().First().Reload();
        }


        public int SaveAll()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new GMEDbConcurrencyException(ex, null, (Entity)ex.Entries.First().Entity);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
