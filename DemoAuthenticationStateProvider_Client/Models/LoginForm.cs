using System.ComponentModel.DataAnnotations;

namespace DemoAuthenticationStateProvider_Client.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
