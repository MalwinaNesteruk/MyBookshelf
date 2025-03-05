using Microsoft.AspNetCore.Mvc;
using MyBookshelf.Models;
using MyBookshelf.Services.Interfaces;

namespace MyBookshelf.Controllers
{
    public class BookController : Controller
    {
        public readonly IGoogleSearchService _googleSearchService;

        public BookController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        [HttpGet]
        public IActionResult SearchBookForm(ListingResponse listing)
        {
            return View(listing);
        }

        [HttpPost]
        public IActionResult SearchBookBase(string query)
        {
            ListingResponse response = new ListingResponse();
            if (query is null || query.Count() < 3)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("SearchBookForm", response);
            }
            List<Book> listBook = _googleSearchService.BaseSearch(query);
            response.books = listBook;
            ViewBag.Query = query;
            return View("SearchBookForm", response);
        }

        [HttpGet]
        public IActionResult AdvancedSearchBookForm(ListingResponse listing)
        {
            return View(listing);
        }

        [HttpPost]
        public IActionResult SearchBookAdvanced(string title, string autors, string publisher, string isbn)
        {
            ListingResponse response = new ListingResponse();
            if (title is null && autors is null && publisher is null && isbn is null)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("AdvancedSearchBookForm", response);
            }
            if (title is not null && title.Count() < 3)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("AdvancedSearchBookForm", response);
            }
            if (autors is not null && autors.Count() < 3)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("AdvancedSearchBookForm", response);
            }
            if (publisher is not null && publisher.Count() < 3)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("AdvancedSearchBookForm", response);
            }
            if (isbn is not null && isbn.Length != 10 && isbn.Length != 13)
            {
                response.errorMessage = "ISBN musi się składać z 10 lub 13 cyfr.";
                return View("AdvancedSearchBookForm", response);
            }

            List<Book> listBook = _googleSearchService.AdvancedSearch(title, autors, publisher, isbn);
            response.books = listBook;
            ViewBag.Title = title;
            ViewBag.Autors = autors;
            ViewBag.Publisher = publisher;
            ViewBag.Isbn = isbn;
            return View("AdvancedSearchBookForm", response);
        }

        [HttpGet]
        public IActionResult BookDetails(Book book)
        {
            // poźniej trzeba będzie przekazywać booka
            return View(book);
        }
    }
}
