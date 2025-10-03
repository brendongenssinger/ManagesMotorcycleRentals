using ManagesMotorcycleRentals.Domain.Enumerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagesMotorcycleRentals.Domain.Entities
{
 
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("uid")]
        public Guid Uid { get; set; }
        [Column("name")]
        [MaxLength(250)]
        public string Name { get; set; }
        [Column("cnpj")]
        [MaxLength(14)]
        public string Cnpj { get; set; }
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }        
        [Column("number_document")]
        public string NumberDocument { get; set; }
        [Column("type_document")]
        public TypeDocument TypeDocument { get; set; }

        [Column("url_document_image")]
        public string UrlDocumentImage { get; set; }
        public MotorcyclesAllocations Rentals { get; set; }
    }

    public static class CustomerFactory
    {
        public static Customer Create(string name, string cnpj, DateTime dateOfBirth, string numberDocument, string urlDocumentImage, TypeDocument typeDocument)
        {
            return new Customer
            {
                Uid = Guid.NewGuid(),
                Name = name,
                Cnpj = cnpj,
                DateOfBirth = dateOfBirth,
                NumberDocument = numberDocument,
                TypeDocument = typeDocument,
                UrlDocumentImage = urlDocumentImage
            };
        }
    }
}
