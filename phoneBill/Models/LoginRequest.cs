using System.ComponentModel.DataAnnotations;

namespace phoneBill.Models
{
    public class LoginRequest
    {
        [Required]
        [Display(Name = "กรุณากรอกรหัสพนักงาน")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "กรุณากรอกรหัสผ่าน")]
        public string Password { get; set; }

        public string CardID {  get; set; }
        public bool RememberMe { get; internal set; }
    }
}
