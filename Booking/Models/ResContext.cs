using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class ResContext : DbContext
    {
        public ResContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Resources> Resources
        {
            get;
            set;
        }

        public DbSet<Booking> Bookings
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resources>()
                .HasMany(a => a.Bookings)
                .WithOne(b => b.Resource)
                .HasForeignKey(b => b.ResourcesId);
        }

    }
}
