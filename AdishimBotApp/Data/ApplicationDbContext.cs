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
            optionsBuilder.UseSqlServer("Data Source=SQL5104.site4now.net;Initial Catalog=db_a73dec_adishimdb;User Id=db_a73dec_adishimdb_admin;Password=Ittipak005!!");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(p => new { p.RuText, p.UrText})
                .IsUnique();
        }
    }
}
