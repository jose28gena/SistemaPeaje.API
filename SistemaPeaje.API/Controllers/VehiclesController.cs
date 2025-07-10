using Microsoft.AspNetCore.Mvc;
using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Services;

namespace SistemaPeaje.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        
        /// <summary>
        /// Get all vehicles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }
        
        /// <summary>
        /// Get vehicle by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(int id)
        {
            var vehicle = await _vehicleService.GetVehicleAsync(id);
            
            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found");
            }
            
            return Ok(vehicle);
        }
        
        /// <summary>
        /// Get vehicle by license plate
        /// </summary>
        [HttpGet("by-license-plate/{licensePlate}")]
        public async Task<ActionResult<VehicleDto>> GetVehicleByLicensePlate(string licensePlate)
        {
            var vehicle = await _vehicleService.GetVehicleByLicensePlateAsync(licensePlate);
            
            if (vehicle == null)
            {
                return NotFound($"Vehicle with license plate {licensePlate} not found");
            }
            
            return Ok(vehicle);
        }
        
        /// <summary>
        /// Create a new vehicle
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateVehicleDto createVehicleDto)
        {
            try
            {
                var vehicle = await _vehicleService.CreateVehicleAsync(createVehicleDto);
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        
        /// <summary>
        /// Update a vehicle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle(int id, UpdateVehicleDto updateVehicleDto)
        {
            try
            {
                var vehicle = await _vehicleService.UpdateVehicleAsync(id, updateVehicleDto);
                
                if (vehicle == null)
                {
                    return NotFound($"Vehicle with ID {id} not found");
                }
                
                return Ok(vehicle);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        
        /// <summary>
        /// Delete a vehicle
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(id);
                
                if (!result)
                {
                    return NotFound($"Vehicle with ID {id} not found");
                }
                
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        
        /// <summary>
        /// Check if vehicle exists by license plate
        /// </summary>
        [HttpGet("exists/{licensePlate}")]
        public async Task<ActionResult<bool>> VehicleExists(string licensePlate)
        {
            var exists = await _vehicleService.VehicleExistsAsync(licensePlate);
            return Ok(exists);
        }
    }
}
