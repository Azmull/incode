using System.ComponentModel.DataAnnotations;

namespace incode.ViewModels
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        
        public string? Notes { get; set; }
    }
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        public int Quantity { get; set; }

        public int UnitPriceAtPurchase { get; set; }
        public bool IsShipped { get; set; }

    }
    public class ProductViewModel
    {
        public int name { get; set; }
    }
}
