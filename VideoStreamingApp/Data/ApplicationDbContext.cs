using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoStreamingApp.Models;

namespace VideoStreamingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
