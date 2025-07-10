using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Repositories
{
    public interface ITollTransactionRepository : IGenericRepository<TollTransaction>
    {
        Task<IEnumerable<TollTransaction>> GetByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<TollTransaction>> GetByTollStationIdAsync(int tollStationId);
        Task<IEnumerable<TollTransaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TollTransaction>> GetTransactionsWithDetailsAsync();
        Task<TollTransaction?> GetTransactionWithDetailsAsync(int id);
    }
}
