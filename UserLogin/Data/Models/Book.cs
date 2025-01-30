using System.ComponentModel.DataAnnotations;

namespace UserLogin.Models
{
    public class Book
    {
        [Key]
        public string ISBN { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }
        public int NoOfPages { get; set; }
        public int Price { get; set; }
        public int quantityInStock { get; set; }
        public string CoverURL { get; set; }
        public string Genre { get; set; }
        public string SubGenre { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishingDate { get; set; }
        public bool NewArrival { get; set; }
        public bool AwardWinner { get; set; }
        public bool? BestSeller { get; set; }
    }
}
