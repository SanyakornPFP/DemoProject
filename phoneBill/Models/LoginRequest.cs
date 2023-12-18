using System.ComponentModel.DataAnnotations;

namespace phoneBill.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; internal set; }
    }
}
