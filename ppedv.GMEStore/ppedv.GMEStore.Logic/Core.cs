using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.GMEStore.Logic
{
    public class Core
    {
        public IUnitOfWork UnitOfWork { get; }

        public Core(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public Company GetCompanyThatPublisheMostGamesOfYear(int year)
        {
            if (year < 0)
                throw new ArgumentException("Das Jahr darf nicht negativ sein");

            var result = UnitOfWork.GameRepository.QueryGamesIncludingAll().Where(x => x.Published.Year == year)
                                                      .GroupBy(x => x.Publisher)
                                                      .OrderByDescending(x => x.Count());
            if (result.Count() == 0)
                return null;
            else
                return result.First().Key;
        }
    }
}
