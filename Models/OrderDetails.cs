using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.Models
{
    public class OrderDetails
    {
        public int SlNo { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid order ID")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid product ID")]
        public int PrdId { get; set; }

        [Required(ErrorMessage = "Order quantity is required")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Order quantity must be at least 1")]
        public int OrderQuantity { get; set; }
    }
}