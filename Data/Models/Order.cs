using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public ICollection<OrderItem> LineItems { get; set; }
        [Required]
        public decimal Total { get; set; }

        public DateTime OrderDate => OrderDate == DateTime.MinValue ? DateTime.Today : OrderDate;
    }
}
