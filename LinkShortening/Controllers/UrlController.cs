using AutoMapper;
using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortening.Presentation.Models;

namespace LinkShortening.Presentation.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _homeService;
        private readonly IMapper _mapper;
        private readonly string _absoluteUri;

        public UrlController(IUrlService homeService, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _homeService = homeService;
            _mapper = mapper;

            var request = httpContextAccessor.HttpContext.Request;
            _absoluteUri = request.Scheme + "://" + request.Host + "/";
        }
        public async Task<IActionResult> Index()
        {
            //В реальном проекте я брал бы данные с сервера постранично
            var data = await _homeService.GetDataAsync();
            var result = _mapper.Map<IEnumerable<UrlBl>, IEnumerable<UrlPl>>(data);

            return View((result, _absoluteUri));
        }

        [HttpGet]
        public async Task<IActionResult> EditCreatePressAsync(int id)
        {
            var data = await _homeService.GetEditPressAsync(id);
            return data != null ?
                View("EditCreate", (_mapper.Map<UrlBl, UrlPl>(data), _absoluteUri)) :
                View("EditCreate", (new UrlPl(), _absoluteUri));
        }

        [HttpPost]
        public async Task<IActionResult> OnCreateUpdateAsync(UrlPl urlPl)
        {
            if (urlPl?.LongUrl == null || !_homeService.IsUrl(urlPl.LongUrl))
                return BadRequest($"Получен некорректный URL: {urlPl?.LongUrl}");

            var result = await _homeService.OnCreateOrFindExistAsync(_mapper.Map<UrlPl, UrlBl>(urlPl));

            return result != null ? View("EditCreate", (_mapper.Map<UrlBl, UrlPl>(result), _absoluteUri)) :
                StatusCode(500, "Внутренняя ошибка сервера: сохранение не удалось.");
        }

        [HttpGet]
        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            var success = await _homeService.OnDeleteAsync(id);
            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: удаление не удалось.");
        }

        [HttpGet]
        public async Task<IActionResult> ShortUrlRequest(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl)) return BadRequest("Не корректная сокращенная ссылка");

            string fullUrl = await _homeService.GetLongUrlAndIncreaseCounter(shortUrl);

            return !string.IsNullOrEmpty(fullUrl) ? Redirect(fullUrl) : NotFound("Полный Url не найден в БД");
        }
    }
}

