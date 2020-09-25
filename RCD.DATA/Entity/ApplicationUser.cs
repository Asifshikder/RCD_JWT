using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
