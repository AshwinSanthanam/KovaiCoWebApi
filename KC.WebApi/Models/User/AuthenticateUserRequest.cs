﻿namespace KC.WebApi.Models.User
{
    public class AuthenticateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
