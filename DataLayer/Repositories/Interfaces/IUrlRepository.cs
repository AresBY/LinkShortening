using LinkShortening.Data.Models;

namespace LinkShortening.Data.Repositories.Interfaces
{
    public interface IUrlRepository : IBaseRepository<UrlDl>
    {
        Task<UrlDl> GetItemByShortUrl(string shortUrl);
        Task<bool> ShortUrlExist(string shortUrl);
    }
}
