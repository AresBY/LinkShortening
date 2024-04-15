using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class UrlRepository : BaseRepository<UrlDl>, IUrlRepository
    {
        public UrlRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<UrlDl> GetItemByShortUrl(string shortUrl)
        {
            return await _entities.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
        }
    }
}
