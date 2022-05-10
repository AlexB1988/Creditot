using Microsoft.EntityFrameworkCore;
using Creditot.Domain.Entities;

namespace Creditot.Domain
{
    public class DataContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<UsersCategories> UsersCategories { get; set; }
        public DbSet<Credits> Credits { get; set; }
        public DbSet<Appeals> Appeals  { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Categories>()
                .HasMany(c => c.Users)
                .WithMany(s => s.Categories)
                .UsingEntity<UsersCategories>(
                j => j
                .HasOne(pt => pt.Users)
                .WithMany(t => t.UsersCategories)
                .HasForeignKey(c => c.UsersId),
                j => j
                .HasOne(pt => pt.Categories)
                .WithMany(t => t.UsersCategories)
                .HasForeignKey(c => c.CategoriesId),
                j =>
                {
                    j.HasKey(t => new { t.Id });
                    j.ToTable("UsersCategories");
                }
                );
        }
    }
}
