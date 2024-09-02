using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Auth
{
    public class UserRegisterRequest : UserBase
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string City { get; set; }

    }
}
