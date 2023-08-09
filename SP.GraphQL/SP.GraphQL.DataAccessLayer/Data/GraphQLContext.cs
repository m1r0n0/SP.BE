using Microsoft.EntityFrameworkCore;

namespace SP.GraphQL.DataAccessLayer.Data
{
    public class GraphQLContext : DbContext
    {
        public GraphQLContext(DbContextOptions<GraphQLContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Customer> Customers { get; }
        public DbSet<Models.Provider> Providers { get; }
        public DbSet<Models.Service> Services { get; }
        public DbSet<Models.Event> Events { get; }
        
    }
}
