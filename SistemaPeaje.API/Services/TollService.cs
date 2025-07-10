using AutoMapper;
using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Models;
using SistemaPeaje.API.Repositories;

namespace SistemaPeaje.API.Services
{
    public class TollService : ITollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TollService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<TollTransactionDto> ProcessTollTransactionAsync(ProcessTollTransactionDto processDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                
                // Get or create vehicle
                var vehicle = await _unitOfWork.Vehicles.GetByLicensePlateAsync(processDto.LicensePlate);
                if (vehicle == null)
                {
                    throw new ArgumentException($"Vehicle with license plate {processDto.LicensePlate} not found");
                }
                
                // Validate toll station
                var tollStation = await _unitOfWork.TollStations.GetByIdAsync(processDto.TollStationId);
                if (tollStation == null || !tollStation.IsActive)
                {
                    throw new ArgumentException($"Toll station with ID {processDto.TollStationId} not found or inactive");
                }
                
                // Calculate toll amount
                var amount = await CalculateTollAmountAsync(processDto.TollStationId, vehicle.VehicleType);
                
                // Create transaction
                var transaction = new TollTransaction
                {
                    VehicleId = vehicle.Id,
                    TollStationId = processDto.TollStationId,
                    TransactionDate = DateTime.UtcNow,
                    Amount = amount,
                    PaymentMethod = processDto.PaymentMethod,
                    PaymentReference = processDto.PaymentReference,
                    Status = TransactionStatus.Completed,
                    Notes = processDto.Notes,
                    CreatedAt = DateTime.UtcNow
                };
                
                await _unitOfWork.TollTransactions.AddAsync(transaction);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                
                // Get complete transaction with related data
                var completeTransaction = await _unitOfWork.TollTransactions.GetTransactionWithDetailsAsync(transaction.Id);
                return _mapper.Map<TollTransactionDto>(completeTransaction);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        
        public async Task<decimal> CalculateTollAmountAsync(int tollStationId, VehicleType vehicleType)
        {
            var tollRate = await _unitOfWork.TollRates.GetAsync(r => 
                r.TollStationId == tollStationId && 
                r.VehicleType == vehicleType && 
                r.IsActive &&
                r.EffectiveDate <= DateTime.UtcNow &&
                (r.ExpirationDate == null || r.ExpirationDate > DateTime.UtcNow));
                
            if (tollRate == null)
            {
                throw new InvalidOperationException($"No active toll rate found for vehicle type {vehicleType} at station {tollStationId}");
            }
            
            return tollRate.Rate;
        }
        
        public async Task<IEnumerable<TollTransactionDto>> GetTransactionsByVehicleAsync(int vehicleId)
        {
            var transactions = await _unitOfWork.TollTransactions.GetByVehicleIdAsync(vehicleId);
            return _mapper.Map<IEnumerable<TollTransactionDto>>(transactions);
        }
        
        public async Task<IEnumerable<TollTransactionDto>> GetTransactionsByStationAsync(int tollStationId)
        {
            var transactions = await _unitOfWork.TollTransactions.GetByTollStationIdAsync(tollStationId);
            return _mapper.Map<IEnumerable<TollTransactionDto>>(transactions);
        }
        
        public async Task<IEnumerable<TollTransactionDto>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _unitOfWork.TollTransactions.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<TollTransactionDto>>(transactions);
        }
        
        public async Task<TollTransactionDto?> GetTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.TollTransactions.GetTransactionWithDetailsAsync(id);
            return transaction != null ? _mapper.Map<TollTransactionDto>(transaction) : null;
        }
    }
}
