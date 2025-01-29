using BookNookAPI.Data.Models;
using BookNookAPI.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookFiltersController : ControllerBase
    {
        public BooksService _booksService;

        public BookFiltersController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("genre/")]
        public IActionResult GetBooksByGenre([FromQuery] List<string> genre, [FromQuery] List<string> subgenre)
        {
            List<Book> books = _booksService.FilterBooksByGenre(genre, subgenre);
            return Ok(books);
        }


        [HttpGet("lang/{language}")]
        public IActionResult GetBooksByLanguage(string language)
        {
            List<Book> books = _booksService.FilterBooksByLanguage(language);
            return Ok(books);
        }


        [HttpGet("author/{author}")]
        public IActionResult GetBooksByAuthor(string author)
        {
            List<Book> books = _booksService.FilterBooksByAuthor(author);
            return Ok(books);
        }


        [HttpGet("price/{max}")]
        public IActionResult GetBooksByPrice(float max)
        {
            List<Book> books = _booksService.FilterBooksByPrice(max);
            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody]Book book)
        {
            _booksService.AddBook(book);
            return Ok();
        }


    }
}
