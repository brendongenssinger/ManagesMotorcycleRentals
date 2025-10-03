namespace ManagesMotorcycleRentals.DTOs
{
    public class MotorCycleCreateDto
    {
        
        public Guid Uid { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public  string NewLicensePlate { get; set; }
    }
}

