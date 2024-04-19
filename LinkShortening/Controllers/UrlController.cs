using AutoMapper;
using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortening.Presentation.Models;
using Microsoft.Extensions.Hosting;

namespace LinkShortening.Presentation.Controllers
{
    //1 Я использовал метод GetAllAsync, что можно делать только при уверенности, что таких записей в БД не будет слишком много.
    //В реальном проекте я брал бы данные постранично и так же выводил бы их на фронт с использованием пагинации.
    //2 Все сообщения в реальном проекте должны быть в неком конфигурационном файле.(сообщения об ошибках и на фронте)
    public class UrlController : Controller
    {
        private readonly IUrlService _homeService;
        private readonly IMapper _mapper;
        private readonly string _adress;
        public UrlController(IUrlService homeService, IMapper mapper, IConfiguration configuration)
        {
            _homeService = homeService;
            _mapper = mapper;
            _adress = configuration["Settings:Adress"];
        }
        public async Task<IActionResult> Index()
        {
            //В реальном проекте я брал бы данные с сервера постранично
            var data = await _homeService.GetDataAsync();
            var result = _mapper.Map<IEnumerable<UrlBl>, IEnumerable<UrlPl>>(data);
            return View((result, _adress));
        }

        [HttpGet]
        public async Task<IActionResult> EditCreatePressAsync(int id)
        {
            var data = await _homeService.GetEditPressAsync(id);
            return data != null ? 
                View("EditCreate", (_mapper.Map<UrlBl, UrlPl>(data), _adress)) :
                View("EditCreate", (new UrlPl(), _adress));
        }


        [HttpPost]
        public async Task<IActionResult> OnCreateUpdateAsync(UrlPl urlPl)
        {
            if (!_homeService.IsUrl(urlPl.LongUrl))
                return BadRequest($"Получен некорректный URL: {urlPl.LongUrl}");

            var result = await _homeService.OnCreateOrFindExistAsync(_mapper.Map<UrlPl, UrlBl>(urlPl));

            return result != null ? View("EditCreate", (_mapper.Map<UrlBl, UrlPl>(result), _adress)) :
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

