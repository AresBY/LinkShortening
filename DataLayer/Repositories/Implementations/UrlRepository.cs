using Data.Models;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.Repositories.Implementations
{
    public class UrlRepository : BaseRepository<UrlDl>, IUrlRepository
    {
        public UrlRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<string> GetFullUrlByShortUrl(string shortUrl)
        {
            var entity = await _entities.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
            return entity?.LongUrl;
        }
    }
}
