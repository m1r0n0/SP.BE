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

        public DbSet<User> UserList { get; set; } = default!;
    }
}
