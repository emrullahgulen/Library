using Business.Absract;
using Business.Concrete;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookservice;
        private readonly IBookTransactionService _borrowedBookService;

        public BookController(IBookService bookservice)
        {
            _bookservice = bookservice;
        }

        [HttpGet]
        public IActionResult GetBookStatusList()
        {
            var result = _bookservice.GetBookStatusList();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult GetBookList()
        {
            var result = _bookservice.GetBookList();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpGet]
        public IActionResult GetBookById(int Id)
        {
            var result = _bookservice.GetBookById(Id);
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            var result = _bookservice.AddBook(book);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult AddBorrowedBook(Book book)
        {
            var result = _bookservice.AddBorrowedBook(book);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet]
        public IActionResult TakeBackBorrowedBook(int Id)
        {
            var result = _bookservice.TakeBackBorrowedBook(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult UpdateBook(Book book)
        {
            var result = _bookservice.UpdateBook(book);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult DeleteBookbyId(int Id)
        {
            var result = _bookservice.RemoveBookById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }


}
