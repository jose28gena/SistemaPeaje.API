using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Services
{
    public interface ITollService
    {
        Task<TollTransactionDto> ProcessTollTransactionAsync(ProcessTollTransactionDto processDto);
        Task<decimal> CalculateTollAmountAsync(int tollStationId, VehicleType vehicleType);
        Task<IEnumerable<TollTransactionDto>> GetTransactionsByVehicleAsync(int vehicleId);
        Task<IEnumerable<TollTransactionDto>> GetTransactionsByStationAsync(int tollStationId);
        Task<IEnumerable<TollTransactionDto>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<TollTransactionDto?> GetTransactionAsync(int id);
    }
}
