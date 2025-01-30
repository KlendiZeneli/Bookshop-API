using Microsoft.EntityFrameworkCore;
using UserLogin.Data;
using UserLogin.Models;

namespace UserLogin.Services
{
    public class BooksService
    {
        private readonly ApplicationDbContext _context;

        public BooksService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Filtering Functions --------------------------------------------

        public async Task<List<Book>> FilterBooksByGenreAsync(List<string> genres, List<string> subgenres)
        {
            List<Book> books = await _context.Books.ToListAsync();
            List<Book> filteredBooks = new List<Book>();

            foreach (var book in books)
            {
                if (genres.Contains(book.Genre) || subgenres.Contains(book.SubGenre))
                {
                    filteredBooks.Add(book);
                }
            }

            return filteredBooks;
        }

        public async Task<List<Book>> FilterBooksByLanguageAsync(string language)
        {
            List<Book> books = await _context.Books.ToListAsync();
            List<Book> filteredBooks = books.Where(book => book.Language == language).ToList();
            return filteredBooks;
        }

        public async Task<List<Book>> FilterBooksByAuthorAsync(string author)
        {
            List<Book> books = await _context.Books.ToListAsync();
            List<Book> filteredBooks = books.Where(book => book.Author == author).ToList();
            return filteredBooks;
        }

        public async Task<List<Book>> FilterBooksByPriceAsync(float max)
        {
            List<Book> books = await _context.Books.ToListAsync();
            List<Book> filteredBooks = books.Where(book => book.Price <= max).ToList();
            return filteredBooks;
        }

        public async Task<List<Book>> FilterBooksByPublicationYearAsync(int yearStart, int yearEnd)
        {
            List<Book> books = await _context.Books.ToListAsync();
            List<Book> filteredBooks = books.Where(book => book.PublishingDate.Year >= yearStart && book.PublishingDate.Year <= yearEnd).ToList();
            return filteredBooks;
        }

        // Admin Page Functions ---------------------------------------

        public async Task<Book> AddBookAsync(Book book)
        {
            var _book = new Book()
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                NoOfPages = book.NoOfPages,
                Price = book.Price,
                CoverURL = book.CoverURL,
                Genre = book.Genre,
                SubGenre = book.SubGenre,
                Publisher = book.Publisher,
                PublishingDate = book.PublishingDate,
                NewArrival = book.NewArrival,
                AwardWinner = book.AwardWinner,
                BestSeller = book.BestSeller,
                Language = book.Language
            };

            await _context.Books.AddAsync(_book);
            await _context.SaveChangesAsync();
            return _book;
        }

        public async Task<bool> RemoveBookAsync(string ISBN)
        {
            var _book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var _book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == book.ISBN);
            if (_book != null)
            {
                _book.ISBN = book.ISBN;
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.Author = book.Author;
                _book.NoOfPages = book.NoOfPages;
                _book.Price = book.Price;
                _book.CoverURL = book.CoverURL;
                _book.Genre = book.Genre;
                _book.SubGenre = book.SubGenre;
                _book.Publisher = book.Publisher;
                _book.PublishingDate = book.PublishingDate;
                _book.NewArrival = book.NewArrival;
                _book.AwardWinner = book.AwardWinner;
                _book.BestSeller = book.BestSeller;
                _book.Language = book.Language;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task AddBooksAsync(List<Book> books)
        {
            _context.Books.AddRange(books); // Add multiple books
            await _context.SaveChangesAsync(); // Save changes asynchronously
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Book?> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        }

    }
}
