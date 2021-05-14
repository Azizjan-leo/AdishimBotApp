using Microsoft.EntityFrameworkCore;
using AdishimBotApp.Models;

namespace AdishimBotApp
{
    public class ApplicationDbContext : DbContext
    {       
        public DbSet<Word> Words { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(p => new { p.RuText, p.UrText})
                .IsUnique();
        }
    }
}
