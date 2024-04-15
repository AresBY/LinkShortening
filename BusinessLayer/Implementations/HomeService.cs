using AutoMapper;
using Business.Interfaces;
using BusinessLayer.Implementations;
using BusinessLayer.Models;
using DataLayer.Models;
using DataLayer.Repositories.Interfaces;
using System.Text;

namespace Business.Implementations
{
    public class HomeService : BaseService<UrlBl, UrlDl>, IHomeService
    {
        public HomeService(IBaseRepository<UrlDl> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<bool> OnDeleteAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<IEnumerable<UrlBl>> GetDataAsync()
        {
            return await GetAllAsync();
        }

        public async Task<UrlBl> GetEditPressAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<bool> OnCreateAsync(UrlBl data)
        {
            data.Creation = DateTime.Now;
            return await AddAsync(data);
        }

        public async Task<bool> OnUpdateAsync(UrlBl data)
        {
            return await UpdateAsync(data);
        }
        public string GenerateShortUrl()
        {
            string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int ShortUrlLength = 6;

            Random random = new Random();
            StringBuilder shortUrlBuilder = new StringBuilder();
            for (int i = 0; i < ShortUrlLength; i++)
            {
                int randomIndex = random.Next(Characters.Length);
                shortUrlBuilder.Append(Characters[randomIndex]);
            }
            return shortUrlBuilder.ToString();
        }
    }
}
