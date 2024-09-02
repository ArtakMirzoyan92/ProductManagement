namespace BusinessLayer.Models.Auth
{
    public class UserResponse : UserBase
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string City { get; set; }
    }
}
