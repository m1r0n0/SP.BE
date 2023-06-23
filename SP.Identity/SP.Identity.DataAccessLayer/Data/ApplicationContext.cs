using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP.Identity.DataAccessLayer.Models;

namespace SP.Identity.DataAccessLayer.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> UserList { get; set; } = default!;
    }
}
