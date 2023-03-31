﻿using Microsoft.AspNetCore.Identity;

namespace DunamicRoleAuth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
       
    }
}
