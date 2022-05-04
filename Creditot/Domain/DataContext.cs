using Creditot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Creditot.Domain
{
    public class DataContext:DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
    }
}
