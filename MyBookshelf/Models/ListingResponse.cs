namespace MyBookshelf.Models
{
    public class ListingResponse
    {
        public List<Book> books { get; set; }
        public string errorMessage { get; set; }
        public int CurrentPage { get; set; } = 1;   // Aktualna strona (domyślnie 1)
        public int TotalPages { get; set; }         // Całkowita liczba stron
        public int PageSize { get; set; } = 10;     // Liczba wyników na stronę (domyślnie 10)
        public int TotalResults { get; set; }       // Całkowita liczba wyników
    }
}
