using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.Models
{
    public class Admin
    {
        public int AId { get; set; }

        [Required(ErrorMessage = "Admin name is required")]
        [StringLength(50)]
        public string AName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Password must be between 6 and 50 characters")]
        public string APassword { get; set; }
    }
}