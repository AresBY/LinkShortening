using AutoMapper;
using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortening.Presentation.Models;

namespace LinkShortening.Presentation.Controllers
{
    //1 Я использовал метод GetAllAsync, что можно делать только при уверенности, что таких записей в БД не будет слишком много.
    //В реальном проекте я брал бы данные постранично и так же выводил бы их на фронт с использованием пагинации.
    //2 Все сообщения в реальном проекте должны быть в неком конфигурационном файле.(сообщения об ошибках и на фронте)
    public class UrlController : Controller
    {
        private readonly IUrlService _homeService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UrlController(IUrlService homeService, IMapper mapper, IConfiguration configuration)
        {
            _homeService = homeService;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            //В реальном проекте я брал бы данные с сервера постранично
            var data = await _homeService.GetDataAsync();
            var result = _mapper.Map<IEnumerable<UrlBl>, IEnumerable<UrlPl>>(data);
            return View(( result, _configuration["Settings:Adress"]));
        }

        [HttpGet]
        public IActionResult CreatePress()
        {
            var data = new UrlPl();
            return View("EditCreate", data);
        }

        [HttpGet]
        public async Task<IActionResult> EditPressAsync(int id)
        {
            var data = await _homeService.GetEditPressAsync(id);

            if (data == null) return StatusCode(500, "Внутренняя ошибка сервера: обьект не найден в БД.");

            var result = _mapper.Map<UrlBl, UrlPl>(data);
            return View("EditCreate", result);
        }

        [HttpPost]
        public async Task<IActionResult> OnCreateAsync(UrlPl urlPl)
        {
            if (!_homeService.IsUrl(urlPl.LongUrl))
                return BadRequest($"Получен некорректный URL: {urlPl.LongUrl}");

            var data = _mapper.Map<UrlPl, UrlBl>(urlPl);
            bool success = await _homeService.OnCreateAsync(data);

            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: сохранение не удалось.");
        }

        [HttpPost]
        public async Task<IActionResult> OnUpdateAsync(UrlPl urlPl)
        {
            if (!_homeService.IsUrl(urlPl.LongUrl))
                return BadRequest($"Получен некорректный URL: {urlPl.LongUrl}");

            var data = _mapper.Map<UrlPl, UrlBl>(urlPl);
            bool success = await _homeService.OnUpdateAsync(data);

            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: обновление не удалось.");
        }

        [HttpGet]
        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            var success = await _homeService.OnDeleteAsync(id);
            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: удаление не удалось.");
        }

        [HttpGet]
        public async Task<IActionResult> CreateShortenUrl()
        {
            string shortenedUrl = await _homeService.GenerateShortUrl();
            return Json(new { shortenedUrl });
        }

        [HttpGet]
        public async Task<IActionResult> ShortUrlRequest(string shortUrl)
        {
            if (shortUrl == null) return BadRequest("Короткий Url имеет значение null");
            string fullUrl = await _homeService.GetFullUrlAndIncreaseCounter(shortUrl);
         
            return !string.IsNullOrEmpty(fullUrl) ?  Redirect(fullUrl) : NotFound("Полный Url не найден в БД");
        }
    }
}

