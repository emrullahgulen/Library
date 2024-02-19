using Entity.Entities;
using Entity.Enums;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class AuthorsController : MyBaseController
    {
        private readonly ILogger<AuthorsController> _logger;
        public AuthorsController(ILogger<AuthorsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var result = NewhttpClinetGet("Author/GetAuthorList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<Author> bookListModels = JsonConvert.DeserializeObject<List<Author>>(data);

                    return View(bookListModels);
                }

                else
                {
                    ViewBag.LoginError = result.StatusCode;
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        [HttpGet]
        public async Task<IActionResult> AddAuthor()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> UpdateAuthor(int Id)
        {
            try
            {
                var result = NewhttpClinetGet($"Author/GetAuthorById?Id={Id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    Author model = JsonConvert.DeserializeObject<Author>(data);
                    return View(model);
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }


        [HttpPost]
        public async Task<IActionResult> UpdateAuthor(Author model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model Valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }
                var result = NewhttpClinetPost("Author/UpdateAuthor", model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAuthors", "Authors");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }



        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model Valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Author/AddAuthor", model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAuthors", "Authors");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            try
            {
  
                var result = NewhttpClinetGet($"Author/DeleteAuthorbyId?Id={Id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    // Burda mesaj gösterilebilir
                    return RedirectToAction("GetAuthors");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }
    }
}
