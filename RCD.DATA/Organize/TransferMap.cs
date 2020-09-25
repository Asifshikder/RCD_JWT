using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Organize
{
    public class TransferMap
    {
        public TransferMap(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }
}
