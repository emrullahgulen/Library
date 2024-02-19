using Business.Absract;
using Business.Concrete;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorservice;

        public AuthorController(IAuthorService authorservice)
        {
            _authorservice = authorservice;
        }

        [HttpGet]
        public IActionResult GetAuthorList()
        {
            var result = _authorservice.GetAuthorList();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost]
        public IActionResult AddAuthor(Author Author)
        {
            var result = _authorservice.AddAuthor(Author);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult UpdateAuthor(Author Author)
        {
            var result = _authorservice.UpdateAuthor(Author);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult DeleteAuthorbyId(int Id)
        {
            var result = _authorservice.RemoveAuthorById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet]
        public IActionResult GetAuthorById(int Id)
        {
            var result = _authorservice.GetAuthorById(Id);
            return Ok(result.Data);
        }
    }
}
