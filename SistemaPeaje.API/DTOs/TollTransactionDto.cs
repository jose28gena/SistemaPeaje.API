using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.DTOs
{
    public class TollTransactionDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int TollStationId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public TransactionStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Related data
        public VehicleDto? Vehicle { get; set; }
        public TollStationDto? TollStation { get; set; }
    }
    
    public class CreateTollTransactionDto
    {
        public int VehicleId { get; set; }
        public int TollStationId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public string? Notes { get; set; }
    }
    
    public class ProcessTollTransactionDto
    {
        public string LicensePlate { get; set; } = string.Empty;
        public int TollStationId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public string? Notes { get; set; }
    }
}
