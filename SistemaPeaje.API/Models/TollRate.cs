using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPeaje.API.Models
{
    public class TollRate
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int TollStationId { get; set; }
        
        [Required]
        public VehicleType VehicleType { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Rate { get; set; }
        
        [StringLength(100)]
        public string? Description { get; set; }
        
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? ExpirationDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        [ForeignKey("TollStationId")]
        public virtual TollStation TollStation { get; set; } = null!;
    }
}
