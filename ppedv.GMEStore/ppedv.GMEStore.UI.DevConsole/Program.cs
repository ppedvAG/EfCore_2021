using ppedv.GMEStore.Logic;
using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace ppedv.GMEStore.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //injection per Hand per Refection
            var assPath = @"C:\Users\Fred\source\repos\EfCore_2021\ppedv.GMEStore\ppedv.GMEStore.Data.EFCore\bin\Debug\net5.0\ppedv.GMEStore.Data.EFCore.dll";
            var ass = Assembly.LoadFrom(assPath);
            var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
            var efRepoInst =  (IRepository)Activator.CreateInstance(typeMitRepo);
            var core = new Core(efRepoInst); 

            //injection per Hand und direkt
            //var core = new Core(new Data.EFCore.EfRepository()); 

            //foreach (var g in core.Repository.Query<Game>().Where(x => x.Name.ToLower().Contains("e")).OrderBy(x => x.Published))
            foreach (var g in core.Repository.QueryGamesIncludingAll().Where(x => x.Name.ToLower().Contains("e")).OrderBy(x => x.Published))
            {
                Console.WriteLine($"{g.Name} Publisher: {g.Publisher?.Name} Dev: {g.Developer?.Name}");
            }
        }
    }
}
