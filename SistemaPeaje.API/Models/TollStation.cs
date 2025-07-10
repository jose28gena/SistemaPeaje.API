using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPeaje.API.Models
{
    public class TollStation
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Column(TypeName = "decimal(10,6)")]
        public decimal? Latitude { get; set; }
        
        [Column(TypeName = "decimal(10,6)")]
        public decimal? Longitude { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
        public virtual ICollection<TollRate> TollRates { get; set; } = new List<TollRate>();
    }
}
