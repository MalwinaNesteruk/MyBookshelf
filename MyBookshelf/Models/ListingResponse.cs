namespace MyBookshelf.Models
{
    public class ListingResponse
    {
        public List<Book> books { get; set; }
        public List<Book> nextBooks { get; set; }
        public string errorMessage { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
