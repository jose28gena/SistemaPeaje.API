using System.ComponentModel.DataAnnotations;

namespace SistemaPeaje.API.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;
        
        [Required]
        public VehicleType VehicleType { get; set; }
        
        [StringLength(50)]
        public string? Brand { get; set; }
        
        [StringLength(50)]
        public string? Model { get; set; }
        
        public int? Year { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
    }
}
