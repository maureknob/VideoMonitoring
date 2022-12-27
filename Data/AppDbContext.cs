using Microsoft.EntityFrameworkCore;
using VideoMonitoring.Models;

namespace VideoMonitoring.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>()
                .HasMany(s => s.Videos)
                .WithOne(v => v.Server);
        }
    }
}
