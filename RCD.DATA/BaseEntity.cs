using System;

namespace RCD.DATA
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? AddDate { get; set; }
        public int? Status { get; set; }
    }
}
