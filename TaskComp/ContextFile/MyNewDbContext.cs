using Microsoft.EntityFrameworkCore;
using TaskComp.Models;

namespace TaskComp.ContextFile
{
    public class MyNewDbContext:DbContext
    {
        public MyNewDbContext(DbContextOptions<MyNewDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductMaster> Product { get; set; }
        public DbSet<CategoryMaster> category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaster>()
            .HasOne(p => p.categorymasters)
            .WithMany(c => c.productmasters)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); //
        }

    }
}
