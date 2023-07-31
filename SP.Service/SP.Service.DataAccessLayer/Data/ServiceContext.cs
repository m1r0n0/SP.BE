using Microsoft.EntityFrameworkCore;
using SP.Service.DataAccessLayer.Models;

namespace SP.Service.DataAccessLayer.Data
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DataAccessLayer.Models.Service> Services { get; set; } = default!;
        public DbSet<Event> Events { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DataAccessLayer.Models.Service>().HasIndex(p => p.ProviderId );
        }
    }
}
