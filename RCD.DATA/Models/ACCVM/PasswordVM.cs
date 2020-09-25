using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models.ACCVM
{
   public class PasswordVM
    {
        public string Email { get; set; }
        public string UserID { get; set; }
        public string BaseCode { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }

        public string Errormessage { get; set; }
    }
}
