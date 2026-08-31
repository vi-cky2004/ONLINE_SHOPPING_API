using System.ComponentModel.DataAnnotations;

namespace ONLINE_SHOPPING_API.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public int CustId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}