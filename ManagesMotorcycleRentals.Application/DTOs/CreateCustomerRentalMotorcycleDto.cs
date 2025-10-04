using Amazon.S3.Model;
using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Application.DTOs
{
    public class CreateCustomerRentalMotorcycleDto
    {

        public string Cnpj { get; set; }
        public Guid MotorcycleUId { get; set; }
        public Guid PlanRentalUId { get;set; }
        public DateTime? RentalEndDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime GetDateTimeStart() => DateTime.Now;

        private Motorcycle? _motorcycle;
        private PlanRental? _planRental;
        private Customer? _customer;
        public void SetMotorcycle(Motorcycle? motorcycle)=> _motorcycle = motorcycle;
        public void SetPlanRental(PlanRental? planRental) => _planRental = planRental;
        public void SetCustomer(Customer? customer) => _customer = customer;

        public Motorcycle GetMotorcycle() => _motorcycle;
        public PlanRental GetPlanRental() => _planRental;
        public Customer GetCustomer() => _customer;


        
    }
}
