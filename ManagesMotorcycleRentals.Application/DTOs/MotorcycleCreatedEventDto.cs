namespace ManagesMotorcycleRentals.Application.DTOs
{
    public record MotorcycleCreatedEventDto(Guid Id, string LicensePlate, int Year, string Model);

}
