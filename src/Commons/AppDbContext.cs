using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CooperaSharp_QueryInLoop
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            var connectionString = $"Data Source={Path.Combine(databaseFolder, "Shop.sqlite")}";

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<Order>()
                        .HasKey(o => o.Id);
            
            modelBuilder.Entity<Order>()
                        .Property(o => o.CreationDate)
                        .HasConversion(v => v.ToString("o"), v => DateTime.Parse(v));

            base.OnModelCreating(modelBuilder);
        }
    }
}
