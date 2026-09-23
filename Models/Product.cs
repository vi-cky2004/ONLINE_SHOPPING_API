using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONLINE_SHOPPING_API.Models
{
    public class Product
    {
        public int PrdId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Product name must be between 2 and 50 characters")]
        public string PrdName { get; set; }

        [Required(ErrorMessage = "Product description is required")]
        [StringLength(100,
            ErrorMessage = "Description cannot exceed 100 characters")]
        public string PrdDescription { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Range(0.01, 99999999.99,
            ErrorMessage = "Price must be greater than 0")]
        public decimal PrdPrice { get; set; }

        [Required(ErrorMessage = "Product quantity is required")]
        [Range(0, int.MaxValue,
            ErrorMessage = "Quantity cannot be negative")]
        public int PrdQuantity { get; set; }

        [StringLength(500,
            ErrorMessage = "Image path cannot exceed 500 characters")]
        public string PrdImage { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}