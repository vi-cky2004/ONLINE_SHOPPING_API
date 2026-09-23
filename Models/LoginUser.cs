namespace ONLINE_SHOPPING_API.Models
{
    public class LoginUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string Role { get; set; }
    }
}