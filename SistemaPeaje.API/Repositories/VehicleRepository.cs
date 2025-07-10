using Microsoft.EntityFrameworkCore;
using SistemaPeaje.API.Data;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TollSystemContext context) : base(context)
        {
        }
        
        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            return await _dbSet.FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
        }
        
        public async Task<IEnumerable<Vehicle>> GetByVehicleTypeAsync(VehicleType vehicleType)
        {
            return await _dbSet.Where(v => v.VehicleType == vehicleType).ToListAsync();
        }
    }
}
