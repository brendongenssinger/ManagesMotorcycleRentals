using Microsoft.AspNetCore.Http;

namespace ManagesMotorcycleRentals.Application.DTOs
{
    public class CreateCustomerDto
    {
        

        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public int CnhType { get; set; }
        public string CnhImage { get; set; }
        public IFormFile? CnhImageFormData { get; set; }

        
        
        
    }
}
