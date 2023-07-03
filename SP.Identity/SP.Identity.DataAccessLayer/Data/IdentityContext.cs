using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.DataAccessLayer.Data
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

        public new DbSet<User> Users { get; set; } = default!;
    }
}
