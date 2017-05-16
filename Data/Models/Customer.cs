using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string EmailAddress { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(5)]
        public string ZipCode { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public string CreditCardNumber { get; set; }
        [NotMapped]
        public string CardCode { get; set; }
        [NotMapped]
        public string ExpirationDate { get; set; }
    }
}
