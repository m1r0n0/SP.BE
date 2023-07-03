using Microsoft.EntityFrameworkCore;

namespace SP.Provider.DataAccessLayer.Data
{
    public class ProviderContext : DbContext
    {
        public ProviderContext(DbContextOptions<ProviderContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Provider> Providers { get; set; } = default!;
    }
}
