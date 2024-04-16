using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Models;
using LinkShortening.Data.Models;

namespace LinkShortening.Business.Interfaces
{
    public interface IUrlService : IBaseService<UrlBl, UrlDl>
    {
        Task<bool> OnDeleteAsync(int id);
        Task<IEnumerable<UrlBl>> GetDataAsync();
        Task<UrlBl> GetEditPressAsync(int id);
        Task<bool> OnCreateAsync(UrlBl data);
        Task<bool> OnUpdateAsync(UrlBl data);
        Task<string> GenerateShortUrl();
        bool IsUrl(string url);
        Task<string> GetFullUrlAndIncreaseCounter(string shortUrl);
        Task<bool> ItemExist(string longUrl);
    }
}
