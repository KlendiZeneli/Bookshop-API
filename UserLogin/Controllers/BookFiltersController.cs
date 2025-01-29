using UserLogin.Models;
using UserLogin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserLogin.Data;

namespace UserLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookFiltersController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly ApplicationDbContext _context;

        public BookFiltersController(BooksService booksService, ApplicationDbContext context)
        {
            _booksService = booksService;
            _context = context;
        }

        // Get books by genre and subgenre
        [HttpGet("genre/")]
        public async Task<IActionResult> GetBooksByGenre([FromQuery] List<string> genre, [FromQuery] List<string> subgenre)
        {
            List<Book> books = await _booksService.FilterBooksByGenreAsync(genre, subgenre);
            return Ok(books);
        }

        // Get books by language
        [HttpGet("lang/{language}")]
        public async Task<IActionResult> GetBooksByLanguage(string language)
        {
            List<Book> books = await _booksService.FilterBooksByLanguageAsync(language);
            return Ok(books);
        }

        // Get books by author
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {
            List<Book> books = await _booksService.FilterBooksByAuthorAsync(author);
            return Ok(books);
        }

        // Get books by price
        [HttpGet("price/{max}")]
        public async Task<IActionResult> GetBooksByPrice(float max)
        {
            List<Book> books = await _booksService.FilterBooksByPriceAsync(max);
            return Ok(books);
        }

        // Add a new book
        [HttpPost("addBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is invalid.");

            var addedBook = await _booksService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookByISBN), new { ISBN = addedBook.ISBN }, addedBook);
        }

        // Remove a book by ISBN
        [HttpDelete("deleteBook/{ISBN}")]
        public async Task<IActionResult> RemoveBook(string ISBN)
        {
            var result = await _booksService.RemoveBookAsync(ISBN);
            if (result)
                return NoContent(); // Successfully deleted, returns 204 No Content
            return NotFound(); // Book not found, returns 404 Not Found
        }

        // Update an existing book
        [HttpPut("updateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is invalid.");

            var result = await _booksService.UpdateBookAsync(book);
            if (result)
                return NoContent(); // Successfully updated, returns 204 No Content
            return NotFound(); // Book not found, returns 404 Not Found
        }

        // Get a book by ISBN
        [HttpGet("getBook/{ISBN}")]
        public async Task<IActionResult> GetBookByISBN(string ISBN)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);
            if (book != null)
                return Ok(book);
            return NotFound(); // Book not found, returns 404 Not Found
        }

        // In BookFiltersController.cs

        [HttpPost("addmultiple")]
        public async Task<IActionResult> AddMultipleBooks([FromBody] List<Book> books)
        {
            if (books == null || !books.Any())
                return BadRequest("Book data is invalid.");

            await _booksService.AddBooksAsync(books);  // Using the new AddBooksAsync method
            return Ok(new { Message = "Books added successfully." });
        }


    }
}
