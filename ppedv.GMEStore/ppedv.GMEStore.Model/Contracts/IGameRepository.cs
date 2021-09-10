using System.Linq;

namespace ppedv.GMEStore.Model.Contracts
{
    public interface IGameRepository : IRepository<Game>
    {
        IQueryable<Game> QueryGamesIncludingAll();
    }
}
