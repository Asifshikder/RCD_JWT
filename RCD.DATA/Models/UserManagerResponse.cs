using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
  public  class UserManagerResponse
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
