using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBookCrud.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tax { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<BookTag>? BookTags {  get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }

    }
}
