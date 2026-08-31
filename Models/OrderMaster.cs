using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.Models
{
    public class OrderMaster
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid customer ID")]
        public int CustId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Order status is required")]
        [StringLength(100)]
        public string OrdStatus { get; set; } = "Under Processing";
    }
}