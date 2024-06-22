using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CallForBids.Models;

namespace CallForBids.Data
{
    public class CallForBidsContext : DbContext
    {
        public CallForBidsContext (DbContextOptions<CallForBidsContext> options)
            : base(options)
        {
        }

        public DbSet<CallForBids.Models.Bids> Bids { get; set; }
        public DbSet<CallForBids.Models.Offices> Offices { get; set; }
        public DbSet<CallForBids.Models.Projects> Projects { get; set; }
        public DbSet<CallForBids.Models.Suppliers> Suppliers { get; set; }
        public DbSet<CallForBids.Models.Documents> Documents { get; set; }
        public DbSet<CallForBids.Models.Roles> Roles { get; set; }
        public DbSet<CallForBids.Models.Submissions> Submissions { get; set; }
        public DbSet<CallForBids.Models.Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bids>()
                .Property(b => b.IsAvailable)
                .HasConversion<byte>(); // Convert boolean to byte (0/1)
            modelBuilder.Entity<Submissions>()
                .Property(b => b.State)
                .HasConversion<byte>();
        }
    }
}
