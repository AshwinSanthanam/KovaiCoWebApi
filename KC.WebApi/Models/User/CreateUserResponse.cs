﻿using System;

namespace KC.WebApi.Models.User
{
    public class CreateUserResponse
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public bool IsSocialLogin { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
