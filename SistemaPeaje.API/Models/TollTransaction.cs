using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPeaje.API.Models
{
    public class TollTransaction
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int VehicleId { get; set; }
        
        [Required]
        public int TollStationId { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        
        [StringLength(100)]
        public string? PaymentReference { get; set; }
        
        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Completed;
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;
        
        [ForeignKey("TollStationId")]
        public virtual TollStation TollStation { get; set; } = null!;
    }
}
