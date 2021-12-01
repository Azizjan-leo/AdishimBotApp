using Microsoft.EntityFrameworkCore;

namespace AdishimBotApp
{
    public class ApplicationDbContext : DbContext
    {       
        public DbSet<Word> Words { get; set; }
        public DbSet<Models.Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApplication2-A8735809-8C3C-4771-B724-2A5A8221C342;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(p => new { p.RuText, p.UrText})
                .IsUnique();
        }
    }
}
