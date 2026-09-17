using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace WebApIPractice.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide user name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide user surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please provide user email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide user password")]
        [MinLength(10)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide user role")]
        public string Role { get; set; }
    }
}
