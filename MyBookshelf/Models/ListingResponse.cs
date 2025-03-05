namespace MyBookshelf.Models
{
    public class ListingResponse
    {
        public List<Book> books { get; set; }
        public string errorMessage { get; set; }
    }
}
