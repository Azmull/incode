using System.ComponentModel.DataAnnotations;

namespace incode.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        public int Quantity { get; set; }

        public int UnitPriceAtPurchase { get; set; }
        
        public bool IsShipped { get; set; }

        public int UserId { get; set; }
        
        public string? Notes { get; set; }
   
        public string name { get; set; }

        public int OrderId { get; set; }
    }
}
