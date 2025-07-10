using Microsoft.EntityFrameworkCore.Storage;
using SistemaPeaje.API.Data;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TollSystemContext _context;
        private IDbContextTransaction? _transaction;
        
        public UnitOfWork(TollSystemContext context)
        {
            _context = context;
            Vehicles = new VehicleRepository(_context);
            TollStations = new GenericRepository<TollStation>(_context);
            TollRates = new GenericRepository<TollRate>(_context);
            TollTransactions = new TollTransactionRepository(_context);
        }
        
        public IVehicleRepository Vehicles { get; private set; }
        public IGenericRepository<TollStation> TollStations { get; private set; }
        public IGenericRepository<TollRate> TollRates { get; private set; }
        public ITollTransactionRepository TollTransactions { get; private set; }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
