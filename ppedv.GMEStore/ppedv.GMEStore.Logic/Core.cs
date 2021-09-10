using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.GMEStore.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repository)
        {
            Repository = repository;
        }

        public Company GetCompanyThatPublisheMostGamesOfYear(int year)
        {
            if (year < 0)
                throw new ArgumentException("Das Jahr darf nicht negativ sein");

            var result = Repository.QueryGamesIncludingAll().Where(x => x.Published.Year == year)
                                                      .GroupBy(x => x.Publisher)
                                                      .OrderByDescending(x => x.Count());
            if (result.Count() == 0)
                return null;
            else
                return result.First().Key;
        }
    }
}
