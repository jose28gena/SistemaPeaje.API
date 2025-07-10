using Microsoft.EntityFrameworkCore;
using SistemaPeaje.API.Data;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Repositories
{
    public class TollTransactionRepository : GenericRepository<TollTransaction>, ITollTransactionRepository
    {
        public TollTransactionRepository(TollSystemContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<TollTransaction>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _dbSet
                .Include(t => t.Vehicle)
                .Include(t => t.TollStation)
                .Where(t => t.VehicleId == vehicleId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TollTransaction>> GetByTollStationIdAsync(int tollStationId)
        {
            return await _dbSet
                .Include(t => t.Vehicle)
                .Include(t => t.TollStation)
                .Where(t => t.TollStationId == tollStationId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TollTransaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(t => t.Vehicle)
                .Include(t => t.TollStation)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TollTransaction>> GetTransactionsWithDetailsAsync()
        {
            return await _dbSet
                .Include(t => t.Vehicle)
                .Include(t => t.TollStation)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        
        public async Task<TollTransaction?> GetTransactionWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Vehicle)
                .Include(t => t.TollStation)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
