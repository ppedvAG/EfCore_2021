using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;

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
            throw new NotImplementedException();
        }
    }
}
