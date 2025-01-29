using BookNookAPI.Data.Models;

namespace BookNookAPI.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context) 
        { 
            _context = context;
        }



        // Filtering Functions --------------------------------------------

        public List<Book> FilterBooksByGenre(List<string> genres, List<string> subgenres)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> filteredBooks = new List<Book>();

            for (int i = 0; i < books.Count; i++)
            {
                for (int j = 0; j < genres.Count; j++)
                {
                    if (books[i].Genre == genres[j])
                        filteredBooks.Add(books[i]);
                }

                for (int j = 0;j < subgenres.Count; j++)
                {
                    if (books[i].SubGenre == subgenres[j])
                        filteredBooks.Add(books[i]);
                    else
                        if (filteredBooks.Contains(books[i]))
                            filteredBooks.Remove(books[i]);
                }
            }

            return filteredBooks;
        }


        public List<Book> FilterBooksByLanguage(string language)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> filteredBooks = new List<Book>();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Language == language)
                    filteredBooks.Add(books[i]);
            }

            return filteredBooks;
        }

        public List<Book> FilterBooksByAuthor(string author)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> filteredBooks = new List<Book>();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Author == author)
                    filteredBooks.Add(books[i]);
            }

            return filteredBooks;
        }

        public List<Book> FilterBooksByPrice(float max)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> filteredBooks = new List<Book>();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Price <= max )
                    filteredBooks.Add(books[i]);
            }

            return filteredBooks;
        }

        public List<Book> FilterBooksByPublicationYear(int yearStart, int yearEnd)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> filteredBooks = new List<Book>();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].PublishingDate.Year >= yearStart && books[i].PublishingDate.Year <= yearEnd)
                    filteredBooks.Add(books[i]);
            }

            return filteredBooks;
        }



        // Admin Page functions ---------------------------------------

        public void AddBook(Book book)
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
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public void RemoveBook(string ISBN)
        {
            var _book = _context.Books.ToList().FirstOrDefault(n =>  n.ISBN == ISBN);

            if (_book != null) {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }

        public void UpdateBook(Book book)
        {
            var _book = _context.Books.ToList().FirstOrDefault(n => n.ISBN == book.ISBN);

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

                _context.SaveChanges();
            }
            
        }
    }
}
