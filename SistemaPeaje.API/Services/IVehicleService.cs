using SistemaPeaje.API.DTOs;

namespace SistemaPeaje.API.Services
{
    public interface IVehicleService
    {
        Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createVehicleDto);
        Task<VehicleDto?> GetVehicleAsync(int id);
        Task<VehicleDto?> GetVehicleByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto?> UpdateVehicleAsync(int id, UpdateVehicleDto updateVehicleDto);
        Task<bool> DeleteVehicleAsync(int id);
        Task<bool> VehicleExistsAsync(string licensePlate);
    }
}
