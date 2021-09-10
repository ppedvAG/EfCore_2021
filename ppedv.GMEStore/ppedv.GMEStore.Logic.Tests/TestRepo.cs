using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.GMEStore.Logic.Tests
{

    public class TestUoW : IUnitOfWork
    {
        public IGameRepository GameRepository => new TestRepo();

        public IRepository<Genre> GenreRepository => throw new NotImplementedException();

        public IRepository<Company> CompanyRepository => throw new NotImplementedException();

        public void ConcurrencyDBWins(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void ConcurrencyUserWins(Entity entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }
    }

    public class TestRepo : IGameRepository
    {
        public void Add(Game entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Game entity)
        {
            throw new NotImplementedException();
        }

        public Game GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Game> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Game> QueryGamesIncludingAll()
        {
            var p1 = new Company() { Name = "P1" };
            var p2 = new Company() { Name = "P2" };

            p1.Published.Add(new Game() { Publisher = p1 });
            p1.Published.Add(new Game() { Publisher = p1 });

            p2.Published.Add(new Game() { Publisher = p2 });
            p2.Published.Add(new Game() { Publisher = p2 });
            p2.Published.Add(new Game() { Publisher = p2 });

            return new[] { p1, p2 }.SelectMany(x => x.Published).AsQueryable();
        }

        public void Update(Game entity)
        {
            throw new NotImplementedException();
        }
    }
}
