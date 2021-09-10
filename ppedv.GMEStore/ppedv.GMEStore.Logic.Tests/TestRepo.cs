using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.GMEStore.Logic.Tests
{
    public class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
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

        public int SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
