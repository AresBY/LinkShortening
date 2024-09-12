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
        Task<UrlBl> OnCreateOrFindExistAsync(UrlBl data);
        Task<bool> OnUpdateAsync(UrlBl data);
        Task<string> GenerateShortUrlAsync();
        bool IsUrl(string url);
        Task<string> GetLongUrlAndIncreaseCounter(string shortUrl);

        Task<UrlBl> GetByLongUrlAsync(string longUrl);
    }
}
