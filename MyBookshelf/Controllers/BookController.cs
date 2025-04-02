using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.Models;
using MyBookshelf.Services.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;
using System.Drawing.Printing;
using MyBookshelf.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBookshelf.Controllers
{
    public class BookController : Controller
    {
        public readonly IGoogleSearchService _googleSearchService;

        public BookController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        //[Authorize] //tu zdjąć to
        [HttpGet]
        public IActionResult SearchBookForm(ListingResponse listing)
        {
            return View(listing);
        }

        [HttpPost]
        public IActionResult SearchBookBase(string query, int page = 1, int maxResults = 10)
        {
            ListingResponse response = new ListingResponse();
            if (query is null || query.Count() < 3)
            {
                response.errorMessage = "Wyszukiwana fraza musi mieć co najmniej 3 znaki.";
                return View("SearchBookForm", response);
            }
            response = _googleSearchService.BaseSearch(query, page, maxResults);
            response.CurrentPage = page;
            response.PageSize = maxResults;
            ViewBag.Query = query;
            return View("SearchBookForm", response);
        }

        //[Authorize] //tu zdjąć to
        [HttpGet]
        public IActionResult AdvancedSearchBookForm(ListingResponse listing)
        {
            return View(listing);
        }

        [HttpPost]
        public IActionResult SearchBookAdvanced(string title, string autors, string publisher, string isbn, int page = 1, int maxResults = 10)
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

            response = _googleSearchService.AdvancedSearch(title, autors, publisher, isbn, page, maxResults);
            ViewBag.Title = title;
            ViewBag.Autors = autors;
            ViewBag.Publisher = publisher;
            ViewBag.Isbn = isbn;
            response.CurrentPage = page;
            response.PageSize = maxResults;
            return View("AdvancedSearchBookForm", response);
        }

        [HttpGet]
        public IActionResult BookDetails(Book book)
        {
            return View(book);
        }
    }
}
