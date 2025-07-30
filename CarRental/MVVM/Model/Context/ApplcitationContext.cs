using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CarRental.MVVM.Model.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Car> Cars { get; set; } = null;
        public DbSet<Booking> Bookings { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
           .HasOne(b => b.User)
           .WithMany(u => u.Bookings)
           .HasForeignKey(b => b.UserID);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CarID);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-IJ8HJGP;Database=CarRental;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public List<User> GetUsers()
        {
            var context = new ApplicationDataContext();
            return context.Users.ToList();
        }

        
    }
}
