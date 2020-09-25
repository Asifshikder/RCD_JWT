using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RCD.DATA.Entity;
using RCD.DATA.Organize;
using System;

namespace RCD.REPO
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ReebuxMap(modelBuilder.Entity<Reebux>());
            new OrderMap(modelBuilder.Entity<Order>());
            new WalletMap(modelBuilder.Entity<Wallet>());
            new RefundMap(modelBuilder.Entity<Refund>());
            new TransactionMap(modelBuilder.Entity<Transaction>());
            new TransferMap(modelBuilder.Entity<Transfer>());
        }
    }
}