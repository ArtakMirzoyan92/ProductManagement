using BusinessLayer.Constants;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Auth
{
    public class UserBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 8, ErrorMessage = ErrorMessages.ValidatePasswordLength)]
        public string PasswordHash { get; set; }
    }
}
