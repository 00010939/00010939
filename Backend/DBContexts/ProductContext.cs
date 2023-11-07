using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBContexts
{
    public class ProductContext : DbContext
    {

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {


        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
