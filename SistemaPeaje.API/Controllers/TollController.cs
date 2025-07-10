using Microsoft.AspNetCore.Mvc;
using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Services;

namespace SistemaPeaje.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TollController : ControllerBase
    {
        private readonly ITollService _tollService;
        
        public TollController(ITollService tollService)
        {
            _tollService = tollService;
        }
        
        /// <summary>
        /// Process a toll transaction
        /// </summary>
        [HttpPost("process")]
        public async Task<ActionResult<TollTransactionDto>> ProcessTollTransaction(ProcessTollTransactionDto processDto)
        {
            try
            {
                var transaction = await _tollService.ProcessTollTransactionAsync(processDto);
                return Ok(transaction);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Get toll amount calculation
        /// </summary>
        [HttpGet("calculate/{tollStationId}/{vehicleType}")]
        public async Task<ActionResult<decimal>> CalculateTollAmount(int tollStationId, int vehicleType)
        {
            try
            {
                var amount = await _tollService.CalculateTollAmountAsync(tollStationId, (Models.VehicleType)vehicleType);
                return Ok(amount);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Get transaction by ID
        /// </summary>
        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<TollTransactionDto>> GetTransaction(int id)
        {
            var transaction = await _tollService.GetTransactionAsync(id);
            
            if (transaction == null)
            {
                return NotFound($"Transaction with ID {id} not found");
            }
            
            return Ok(transaction);
        }
        
        /// <summary>
        /// Get transactions by vehicle ID
        /// </summary>
        [HttpGet("transactions/vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<TollTransactionDto>>> GetTransactionsByVehicle(int vehicleId)
        {
            var transactions = await _tollService.GetTransactionsByVehicleAsync(vehicleId);
            return Ok(transactions);
        }
        
        /// <summary>
        /// Get transactions by toll station ID
        /// </summary>
        [HttpGet("transactions/station/{tollStationId}")]
        public async Task<ActionResult<IEnumerable<TollTransactionDto>>> GetTransactionsByStation(int tollStationId)
        {
            var transactions = await _tollService.GetTransactionsByStationAsync(tollStationId);
            return Ok(transactions);
        }
        
        /// <summary>
        /// Get transactions by date range
        /// </summary>
        [HttpGet("transactions/date-range")]
        public async Task<ActionResult<IEnumerable<TollTransactionDto>>> GetTransactionsByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be greater than end date");
            }
            
            var transactions = await _tollService.GetTransactionsByDateRangeAsync(startDate, endDate);
            return Ok(transactions);
        }
    }
}
