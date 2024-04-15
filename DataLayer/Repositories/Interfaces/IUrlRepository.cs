using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IUrlRepository : IBaseRepository<UrlDl>
    {
        Task<string> GetFullUrlByShortUrl(string shortUrl);
    }
}
