using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Features;

namespace Selu383.SP24.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().Property(x => x.Name).HasMaxLength(120);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "HolidayInn", Address = "467 street St." },
                new Hotel { Id = 2, Name = "MotelInn", Address = "484 street St." },
                new Hotel { Id = 3, Name = "StayAWhile", Address = "286 stay St." }
                );
        }
    }
}


