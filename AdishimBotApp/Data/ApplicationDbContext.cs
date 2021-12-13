using Microsoft.EntityFrameworkCore;

namespace AdishimBotApp
{
    public class ApplicationDbContext : DbContext
    {       
        public DbSet<Word> Words { get; set; }
        public DbSet<Models.Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=Ni&Thikn3jwu^tg6;Host=localhost;Port=5432;Database=adishim;Pooling=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(p => new { p.RuText, p.UrText})
                .IsUnique();
        }
    }
}
