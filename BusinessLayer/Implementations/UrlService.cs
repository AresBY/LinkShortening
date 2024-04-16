using AutoMapper;
using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Models;
using LinkShortening.Data.Models;
using LinkShortening.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace LinkShortening.Business.Implementations
{
    public class UrlService : BaseService<UrlBl, UrlDl>, IUrlService
    {
        private readonly IUrlRepository _homeRepository;
        private readonly IConfiguration _configuration;
        public UrlService(IUrlRepository repository, IMapper mapper, IConfiguration configuration) : base(repository, mapper)
        {
            _homeRepository = repository;
            _configuration = configuration;
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
        public async Task<string> GetFullUrlAndIncreaseCounter(string shortUrl)
        {
            var data = await _homeRepository.GetItemByShortUrl(shortUrl);
            if (data == null) return null;

            data.TransitionCount += 1;
            await _homeRepository.UpdateAsync(data);
            return data.LongUrl;
        }
        public bool IsUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
        public async Task<string> GenerateShortUrl()
        {
            const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var ShortUrlLength = Convert.ToInt32(_configuration["Settings:ShortUrlLength"]);

            string shortUrl = null;
            int countIteration = 0;
            do
            {
                if (countIteration++ > 100000)
                {
                    throw new Exception("Кол-во итераций цикла в  GenerateShortUrl больше 100000. Вероятен бесконечный цикл");
                }
                Random random = new Random();
                StringBuilder shortUrlBuilder = new StringBuilder();
                for (int i = 0; i < ShortUrlLength; i++)
                {
                    int randomIndex = random.Next(Characters.Length);
                    shortUrlBuilder.Append(Characters[randomIndex]);
                }
                shortUrl = shortUrlBuilder.ToString();
            }
            while (await _homeRepository.ShortUrlExist(shortUrl));

            return shortUrl;
        }
    }
}
