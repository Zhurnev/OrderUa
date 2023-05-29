using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderUA.Models;

namespace OrderUA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Category { get; set; }
		public DbSet<ProductType> ProductType { get; set; }
		public DbSet<Product> Product{ get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }

	}
}
