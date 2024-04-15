using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataLayer.Models;

namespace Business.Interfaces
{
    public interface IHomeService : IBaseService<UrlBl, UrlDl>
    {
        Task<bool> OnDeleteAsync(int id);
        Task<IEnumerable<UrlBl>> GetDataAsync();
        Task<UrlBl> GetEditPressAsync(int id);
        Task<bool> OnCreateAsync(UrlBl data);
        Task<bool> OnUpdateAsync(UrlBl data);
        string GenerateShortUrl();
    }
}
