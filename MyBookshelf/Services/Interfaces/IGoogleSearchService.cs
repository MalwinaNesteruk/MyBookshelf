using MyBookshelf.Models;

namespace MyBookshelf.Services.Interfaces
{
    public interface IGoogleSearchService
    {
        ListingResponse BaseSearch(string query, int page = 1, int maxResults = 10);
        List<Book> AdvancedSearch(string title, string autors, string publisher, string isbn, int page = 1, int maxResults = 10);
        int GetTotalResultsAdvanced(string title, string autors, string publisher, string isbn);
    }
}
