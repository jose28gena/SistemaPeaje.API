namespace SistemaPeaje.API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository Vehicles { get; }
        IGenericRepository<Models.TollStation> TollStations { get; }
        IGenericRepository<Models.TollRate> TollRates { get; }
        ITollTransactionRepository TollTransactions { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
