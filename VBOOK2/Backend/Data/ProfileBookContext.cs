using Microsoft.EntityFrameworkCore;
using VBOOK2.Backend.Models;

namespace VBOOK2.Backend.Data
{
    public class ProfileBookContext : DbContext
    {
        public ProfileBookContext(DbContextOptions<ProfileBookContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
