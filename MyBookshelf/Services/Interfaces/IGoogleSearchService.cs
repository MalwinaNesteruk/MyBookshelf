using MyBookshelf.Models;

namespace MyBookshelf.Services.Interfaces
{
    public interface IGoogleSearchService
    {
        List<Book> BaseSearch(string query);
        List<Book> AdvancedSearch(string title, string autors, string publisher, string isbn);
    }
}
