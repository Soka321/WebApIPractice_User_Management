using System.ComponentModel.DataAnnotations;

namespace WebApIPractice.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please provide user email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide user password")]
        //[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
    }
}
