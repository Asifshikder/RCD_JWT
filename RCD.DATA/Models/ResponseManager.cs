using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class ResponseManager
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
