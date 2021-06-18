namespace KC.WebApi.Models.User
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
    }
}
