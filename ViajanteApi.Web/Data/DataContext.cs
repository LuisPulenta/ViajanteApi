using Microsoft.EntityFrameworkCore;
using ViajanteApi.Web.Data.Entities;

namespace ViajanteApi.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Customer> Customers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasIndex(x => x.Name).IsUnique();
        }
    }
}