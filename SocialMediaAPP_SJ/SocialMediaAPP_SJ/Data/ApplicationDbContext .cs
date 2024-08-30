using Microsoft.EntityFrameworkCore;
using SocialMediaAPP_SJ.Model;

namespace SocialMediaAPP_SJ.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> user { get; set; }
        public DbSet<Post> post { get; set; }
        public DbSet<Comment> comment { get; set; }
    }
}
