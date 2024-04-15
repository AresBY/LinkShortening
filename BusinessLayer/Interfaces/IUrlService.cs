using Business.Interfaces;
using Business.Models;
using Data.Models;

namespace Business.Interfaces
{
    public interface IUrlService : IBaseService<UrlBl, UrlDl>
    {
        Task<bool> OnDeleteAsync(int id);
        Task<IEnumerable<UrlBl>> GetDataAsync();
        Task<UrlBl> GetEditPressAsync(int id);
        Task<bool> OnCreateAsync(UrlBl data);
        Task<bool> OnUpdateAsync(UrlBl data);
        string GenerateShortUrl();
        bool IsUrl(string? url);
        Task<string> GetFullUrl(string shortUrl);
    }
}
