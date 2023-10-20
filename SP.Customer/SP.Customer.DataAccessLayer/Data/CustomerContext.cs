using Microsoft.EntityFrameworkCore;

namespace SP.Customer.DataAccessLayer.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
                    : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Models.Customer> Customers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DataAccessLayer.Models.Customer>().HasIndex(p => p.UserId );
        }
    }
}
