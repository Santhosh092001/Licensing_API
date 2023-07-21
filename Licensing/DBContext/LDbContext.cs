using Licensing.Models;
using Microsoft.EntityFrameworkCore;

namespace Licensing.DBContext
{
    public class LDbContext : DbContext
    {
        public LDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCustomerMap> ProductCustomerMaps { get; set; }
        public DbSet<ActivationKey> ActivationKeys { get; set; }

    }
}
