using AutoMapper;
using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Models;
using SistemaPeaje.API.Repositories;

namespace SistemaPeaje.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createVehicleDto)
        {
            // Check if vehicle with license plate already exists
            var existingVehicle = await _unitOfWork.Vehicles.GetByLicensePlateAsync(createVehicleDto.LicensePlate);
            if (existingVehicle != null)
            {
                throw new InvalidOperationException($"Vehicle with license plate {createVehicleDto.LicensePlate} already exists");
            }
            
            var vehicle = _mapper.Map<Vehicle>(createVehicleDto);
            vehicle.CreatedAt = DateTime.UtcNow;
            
            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<VehicleDto>(vehicle);
        }
        
        public async Task<VehicleDto?> GetVehicleAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            return vehicle != null ? _mapper.Map<VehicleDto>(vehicle) : null;
        }
        
        public async Task<VehicleDto?> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByLicensePlateAsync(licensePlate);
            return vehicle != null ? _mapper.Map<VehicleDto>(vehicle) : null;
        }
        
        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }
        
        public async Task<VehicleDto?> UpdateVehicleAsync(int id, UpdateVehicleDto updateVehicleDto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return null;
            }
            
            // Check if license plate is being changed and if it already exists
            if (!string.IsNullOrEmpty(updateVehicleDto.LicensePlate) && 
                updateVehicleDto.LicensePlate != vehicle.LicensePlate)
            {
                var existingVehicle = await _unitOfWork.Vehicles.GetByLicensePlateAsync(updateVehicleDto.LicensePlate);
                if (existingVehicle != null)
                {
                    throw new InvalidOperationException($"Vehicle with license plate {updateVehicleDto.LicensePlate} already exists");
                }
                vehicle.LicensePlate = updateVehicleDto.LicensePlate;
            }
            
            // Update other fields if provided
            if (updateVehicleDto.VehicleType.HasValue)
                vehicle.VehicleType = updateVehicleDto.VehicleType.Value;
            
            if (updateVehicleDto.Brand != null)
                vehicle.Brand = updateVehicleDto.Brand;
                
            if (updateVehicleDto.Model != null)
                vehicle.Model = updateVehicleDto.Model;
                
            if (updateVehicleDto.Year.HasValue)
                vehicle.Year = updateVehicleDto.Year;
                
            if (updateVehicleDto.Color != null)
                vehicle.Color = updateVehicleDto.Color;
            
            vehicle.UpdatedAt = DateTime.UtcNow;
            
            _unitOfWork.Vehicles.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<VehicleDto>(vehicle);
        }
        
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return false;
            }
            
            // Check if vehicle has transactions
            var hasTransactions = await _unitOfWork.TollTransactions.ExistsAsync(t => t.VehicleId == id);
            if (hasTransactions)
            {
                throw new InvalidOperationException("Cannot delete vehicle with existing toll transactions");
            }
            
            _unitOfWork.Vehicles.Delete(vehicle);
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        
        public async Task<bool> VehicleExistsAsync(string licensePlate)
        {
            return await _unitOfWork.Vehicles.ExistsAsync(v => v.LicensePlate == licensePlate);
        }
    }
}
