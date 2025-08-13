using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Data
{
    public class PostsDbContext : DbContext
    {

        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options) 
        {
        }

        // configure relationships between models, configure models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
