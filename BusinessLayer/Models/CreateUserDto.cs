using BusinessLayer.Constants;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }

        public string City { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 8, ErrorMessage = ErrorMessages.ValidatePasswordLength)]
        public string PasswordHash { get; set; }
    }
}
