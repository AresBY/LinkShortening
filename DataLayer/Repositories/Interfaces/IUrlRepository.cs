using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IUrlRepository : IBaseRepository<UrlDl>
    {
        Task<UrlDl> GetItemByShortUrl(string shortUrl);
    }
}
