using ppedv.GMEStore.Logic;
using ppedv.GMEStore.Model;
using System;
using System.Linq;

namespace ppedv.GMEStore.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var core = new Core(new Data.EFCore.EfRepository());

            //foreach (var g in core.Repository.Query<Game>().Where(x => x.Name.ToLower().Contains("e")).OrderBy(x => x.Published))
            foreach (var g in core.Repository.QueryGamesIncludingAll().Where(x => x.Name.ToLower().Contains("e")).OrderBy(x => x.Published))
            {
                Console.WriteLine($"{g.Name} Publisher: {g.Publisher?.Name} Dev: {g.Developer?.Name}");
            }
        }
    }
}
