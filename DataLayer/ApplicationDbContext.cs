using LinkShortening.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortening.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UrlDl>? Urls { get; set; }
    }
}
