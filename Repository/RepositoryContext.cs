using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<Customer>? Customers { get; set; }
    }
}