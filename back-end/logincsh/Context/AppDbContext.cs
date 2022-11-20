using logincsh.Models;
using Microsoft.EntityFrameworkCore;

namespace logincsh.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected void OnModeleCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }//helps to build entity from backend core
    }
}
