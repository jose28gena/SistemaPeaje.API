using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public VehicleType VehicleType { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class CreateVehicleDto
    {
        public string LicensePlate { get; set; } = string.Empty;
        public VehicleType VehicleType { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
    }
    
    public class UpdateVehicleDto
    {
        public string? LicensePlate { get; set; }
        public VehicleType? VehicleType { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
    }
}
