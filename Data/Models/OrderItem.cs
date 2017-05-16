using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data.Models
{
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
