using CatteryRegister.Model;
using Microsoft.EntityFrameworkCore;

namespace CatteryRegister.DataContext
{
    public class CatteryDbContext : DbContext
    {
        public CatteryDbContext(DbContextOptions<CatteryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>(c =>
            {
                c.ToTable("cats", "cr");
                c.Property(x => x.Id).UseHiLo();
                c.HasKey(x => x.Id);
                c.Property(x => x.Name).IsRequired();
                c.HasOne(x => x.Litter)
                    .WithMany();
                c.Property(x => x.DateOfBirth).IsRequired();
                c.Property(x => x.ImageId).IsRequired();
            });
            modelBuilder.Entity<Cattery>(c =>
            {
                c.ToTable("catteries", "cr");
                c.Property(x => x.Id).UseHiLo();
                c.HasKey(x => x.Id);
                c.Property(x => x.Name).IsRequired();
                c.Property(x => x.OwnerId).IsRequired();
                c.HasMany(x => x.Litters)
                    .WithOne(x => x.Cattery);
            });
            modelBuilder.Entity<Litter>(c =>
            {
                c.ToTable("litters", "cr");
                c.Property(x => x.Id).UseHiLo();
                c.HasKey(x => x.Id);
                c.Property(x => x.Code).IsRequired();
                c.Property(x => x.CreationDate).IsRequired();
                c.HasOne(x => x.Cattery)
                    .WithMany(x => x.Litters)
                    .IsRequired();

                c.HasMany<Cat>(x => x.Cats)
                    .WithOne(x => x.Litter);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}