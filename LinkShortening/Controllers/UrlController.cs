using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _homeService;
        protected readonly IMapper _mapper;
        public UrlController(IUrlService homeService, IMapper mapper)
        {
            _homeService = homeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _homeService.GetDataAsync();
            var result = _mapper.Map<IEnumerable<UrlBl>, IEnumerable<UrlPl>>(data);
            return View(result);
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

            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: не удалось обработать запрос.");
        }

        [HttpPost]
        public async Task<IActionResult> OnUpdateAsync(UrlPl urlPl)
        {
            if (!_homeService.IsUrl(urlPl.LongUrl))
                return BadRequest($"Получен некорректный URL: {urlPl.LongUrl}");

            var data = _mapper.Map<UrlPl, UrlBl>(urlPl);
            bool success = await _homeService.OnUpdateAsync(data);

            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: не удалось обработать запрос.");
        }

        [HttpGet]
        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            var success = await _homeService.OnDeleteAsync(id);
            return success ? RedirectToAction("Index") : StatusCode(500, "Внутренняя ошибка сервера: не удалось обработать запрос.");
        }
       
        [HttpGet]
        public IActionResult CreateShortenUrl()
        {
            string shortenedUrl = _homeService.GenerateShortUrl();
            return Json(new { shortenedUrl });
        }

        [HttpGet]
        public async Task<IActionResult> GetFullUrl(string shortUrl)
        {
            string fullUrl = await _homeService.GetFullUrl(shortUrl);
            return Json(new { fullUrl });
        }
    }
}
