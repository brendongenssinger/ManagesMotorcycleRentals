namespace ManagesMotorcycleRentals.API.Messaging.Model
{
    public record MotorcycleMessage
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
