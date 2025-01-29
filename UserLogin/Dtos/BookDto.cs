namespace UserLogin.Dtos
{
    public class BookDto
    {
            public String ISBN{ get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public string CoverURL { get; set; }
            public int QuantityInStock { get; set; }
        
    }
}
