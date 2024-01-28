using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Entities;

namespace Selu383.SP24.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .HasData(new Hotel
                {
                    Id = 1,
                    Name = "Swag",
                    Address = "Swag"
                });
        }
    }
}
