using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Vehicle>> GetByVehicleTypeAsync(VehicleType vehicleType);
    }
}
