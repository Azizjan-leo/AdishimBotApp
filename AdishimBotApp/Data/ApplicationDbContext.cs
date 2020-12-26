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
            optionsBuilder.UseSqlServer("Data Source=SQL5097.site4now.net;Initial Catalog=DB_A6B0FC_AdishimBot;User Id=DB_A6B0FC_AdishimBot_admin;Password=Ittipak005!!");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(p => new { p.RuText, p.UrText})
                .IsUnique();
        }
    }
}
