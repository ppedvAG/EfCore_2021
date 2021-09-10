namespace ppedv.GMEStore.Model.Contracts
{
    public interface IUnitOfWork
    {
        int SaveAll();

        IGameRepository GameRepository { get; }
        IRepository<Genre> GenreRepository { get; }
        IRepository<Company> CompanyRepository { get; }
    }
}
