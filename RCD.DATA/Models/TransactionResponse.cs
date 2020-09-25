using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class TransactionResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int? Status { get; set; }
    }
}
