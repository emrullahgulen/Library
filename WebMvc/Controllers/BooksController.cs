using Entity.Entities;
using Entity.Enums;
using Entity.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
//using System.Web.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers
{

    public class BooksController:MyBaseController
    {

        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BooksController(ILogger<BooksController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBookStatusList()
        {
            try
            {
                var result = NewhttpClinetGet("Book/GetBookStatusList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<BookListModel> bookListModels = JsonConvert.DeserializeObject<List<BookListModel>>(data);
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
        public async Task<IActionResult> GetBookList()
        {
            try
            {
                var result = NewhttpClinetGet("Book/GetBookList");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<BookModel> bookListModels = JsonConvert.DeserializeObject<List<BookModel>>(data);

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
        public async Task<IActionResult> AddBook()
        {
            try
            {
                var bookTypes = new BookTypesListModel
                {
                    EnumList = Enum.GetValues(typeof(BookType))
                           .Cast<BookType>()
                           .Select(e => new SelectListItem
                           {
                               Value = ((int)e).ToString(),
                               Text = e.ToString()
                           })
                };
                ViewBag.BookTypes = bookTypes.EnumList;

                var result = NewhttpClinetGet("Author/GetAuthorList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<Author> model = JsonConvert.DeserializeObject<List<Author>>(data);
                    ViewBag.Authors = model;
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
        public async Task<IActionResult> AddBook(Book model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model Valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Book/AddBook", model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBookList", "Books");
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
        public async Task<IActionResult> UpdateBook(int Id)
        {
            try
            {
                var bookTypes = new BookTypesListModel
                {
                    EnumList = Enum.GetValues(typeof(BookType))
                                .Cast<BookType>()
                                .Select(e => new SelectListItem
                                {
                                    Value = ((int)e).ToString(),
                                    Text = e.ToString()
                                })
                };
                ViewBag.BookTypes = bookTypes.EnumList;
                var result = NewhttpClinetGet("Author/GetAuthorList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<Author> model = JsonConvert.DeserializeObject<List<Author>>(data);
                    ViewBag.Authors = model;
                }

                var resultBook = NewhttpClinetGet($"Book/GetBookById?Id={Id}");
                if (resultBook.IsSuccessStatusCode)
                {
                    var data = await resultBook.Content.ReadAsStringAsync().ConfigureAwait(true);
                    Book model = JsonConvert.DeserializeObject<Book>(data);
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
        public async Task<IActionResult> UpdateBook(Book model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Book/UpdateBook", model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBookList", "Books");
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
        public async Task<IActionResult> DeleteBook(int Id)
        {
            try
            {
                var result = NewhttpClinetGet($"Book/DeleteBookbyId?Id={Id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    // Burda mesaj gösterilebilir
                    return RedirectToAction("GetBookList");
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
        public async Task<IActionResult> AddBorrowedBook(int Id)
        {
            try
            {    
                var result = NewhttpClinetGet("Member/GetMemberList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<Member> model = JsonConvert.DeserializeObject<List<Member>>(data);
                    ViewBag.Members = model;
                }

                var resultBook = NewhttpClinetGet($"Book/GetBookById?Id={Id}");
                if (resultBook.IsSuccessStatusCode)
                {
                    var data = await resultBook.Content.ReadAsStringAsync().ConfigureAwait(true);
                    Book model = JsonConvert.DeserializeObject<Book>(data);
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
        public async Task<IActionResult> AddBorrowedBook(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Book/AddBorrowedBook",book);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBookList", "Books");
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
        public async Task<IActionResult> TakeBackBorrowedBook(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model valid değil");
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetGet($"Book/TakeBackBorrowedBook?Id={Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBookList", "Books");
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

    }
}
