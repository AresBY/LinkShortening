using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Models;
using Data.Repositories.Interfaces;
using System.Text;

namespace Business.Implementations
{
    public class UrlService : BaseService<UrlBl, UrlDl>, IUrlService
    {
        private readonly IUrlRepository _homeRepository;
        public UrlService(IUrlRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _homeRepository = repository;
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
        public string GenerateShortUrl()
        {
            const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //Тут строго установлено значение, его следовало бы вынести в некий конфиг.
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
