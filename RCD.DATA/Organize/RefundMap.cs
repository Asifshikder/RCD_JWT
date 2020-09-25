using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Organize
{
  public  class RefundMap
    {
        public RefundMap(EntityTypeBuilder<Refund> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }
}
