using Microsoft.EntityFrameworkCore;

namespace SP.GraphQL.DataAccessLayer.Data
{
    public class GraphQLContext : DbContext
    {
        public GraphQLContext(DbContextOptions<GraphQLContext> options)
                    : base(options)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Provider> Providers { get; set; }
        public DbSet<Models.Service> Services { get; set; }
        public DbSet<Models.Event> Events { get; set; }
        
    }
}
