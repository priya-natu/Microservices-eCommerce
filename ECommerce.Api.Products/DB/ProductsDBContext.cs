using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DB
{
    public class ProductsDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDBContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
