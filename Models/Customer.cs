using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.Models
{
    public class Customer
    {
        public int CustId { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Customer name must be between 3 and 50 characters")]
        public string CustName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string CustAddress { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Enter a valid 10-digit phone number")]
        public string CustPhone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [StringLength(50)]
        public string CustEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Password must be between 6 and 50 characters")]
        public string CustPassword { get; set; }
    }
}