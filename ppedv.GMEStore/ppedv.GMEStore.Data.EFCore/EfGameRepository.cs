using Microsoft.EntityFrameworkCore;
using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System.Linq;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfGameRepository : EfRepository<Game>, IGameRepository
    {
        public EfGameRepository(EfContext context) : base(context)
        { }

        public IQueryable<Game> QueryGamesIncludingAll()
        {
            return _context.Games.Include(x => x.Genres).Include(x => x.Developer).Include(x => x.Publisher);
        }
    }
}
