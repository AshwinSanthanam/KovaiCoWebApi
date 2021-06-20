namespace KC.WebApi.Models.User
{
    public class AuthenticateExternalUserRequest
    {
        public string IdToken { get; set; }
        public string Provider { get; set; }
    }
}
