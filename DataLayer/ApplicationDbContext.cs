using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UrlDl>? Urls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlDl>().HasData(
                new UrlDl
                {
                    Id = 1,
                    LongUrl = "https://learn.microsoft.com/ru-ru/ef/ef6/modeling/code-first/migrations/automatic",
                    ShortUrl = "fafasf",
                    Creation = new DateTime(),
                    TransitionCount = 3
                },
                new UrlDl
                {
                    Id = 2,
                    LongUrl = "https://learn.microsoft.com/ru-ru/ef/ef6/modeling/code-first/migrations/",
                    ShortUrl = "dasds",
                    Creation = new DateTime(),
                    TransitionCount = 4
                }
            ); ;
        }
    }
}
