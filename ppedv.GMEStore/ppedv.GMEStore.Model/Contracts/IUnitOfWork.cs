namespace ppedv.GMEStore.Model.Contracts
{
    public interface IUnitOfWork
    {
        int SaveAll();

        void ConcurrencyUserWins(Entity entity);
        void ConcurrencyDBWins(Entity entity);


        IGameRepository GameRepository { get; }
        IRepository<Genre> GenreRepository { get; }
        IRepository<Company> CompanyRepository { get; }
    }
}
