using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.DTOs
{
    public class CustomerRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string CustName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200)]
        public string CustAddress { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Enter a valid 10-digit phone number")]
        public string CustPhone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string CustEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6)]
        public string CustPassword { get; set; }
    }
}