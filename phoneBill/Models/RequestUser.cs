using System.ComponentModel.DataAnnotations;

namespace phoneBill.Models
{
    public class RequestUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string FileName { get; set; }

        public IFormFile File { set; get; }

    }
}
