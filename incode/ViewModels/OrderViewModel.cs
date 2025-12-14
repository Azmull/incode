using incode.Models;
using System.ComponentModel.DataAnnotations;

namespace incode.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public bool RequiresShipping { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int UnitPriceAtPurchase { get; set; }

        public bool IsShipped { get; set; }

        public virtual ICollection<ShippedDetail> ShippedDetails { get; set; } = new List<ShippedDetail>();
    }


}
