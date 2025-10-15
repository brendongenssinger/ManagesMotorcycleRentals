namespace ManagesMotorcycleRentals.Application.DTOs
{
    public record MotorcycleCreatedEventDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; } 
        public int Year { get; set; } 
        public string Model { get; set; } 
    }
}
