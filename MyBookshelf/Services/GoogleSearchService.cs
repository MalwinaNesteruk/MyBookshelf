using Microsoft.AspNetCore.Http;
using MyBookshelf.Models;
using MyBookshelf.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace MyBookshelf.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        public List<Book> BaseSearch(string query, int page = 1, int maxResults = 10)
        {
            int startIndex = (page - 1) * maxResults;
            string url = $"https://www.googleapis.com/books/v1/volumes?q={query}&startIndex={startIndex}&maxResults={maxResults}";
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Execute(request, Method.Get);

            if (response.IsSuccessful)
            {
                var contentJson = response.Content;
                GoogleBaseSearchResponse googleBaseSearchResponse = JsonConvert.DeserializeObject<GoogleBaseSearchResponse>(contentJson);
                List<Book> listBook = new List<Book>();

                foreach (var book in googleBaseSearchResponse.Items ?? new List<Item>())
                {
                    var newBook = new Book()
                    {
                        Id = book.Id,
                        Title = book.VolumeInfo.Title,
                        Authors = book.VolumeInfo.Authors is null ? null : string.Join(", ", book.VolumeInfo.Authors),
                        PublishedDate = book.VolumeInfo.PublishedDate is null ? null : DateOnly.Parse(
                            book.VolumeInfo.PublishedDate.Contains('-') ?
                            book.VolumeInfo.PublishedDate.ToString() : book.VolumeInfo.PublishedDate + "-01-01").ToString(),
                        PageCount = book.VolumeInfo.PageCount,
                        Publisher = book.VolumeInfo.Publisher is not null ? book.VolumeInfo.Publisher : "brak danych",
                        PrintType = book.VolumeInfo.PrintType,
                        Description = book.VolumeInfo.Description,
                        Price = book.SaleInfo.Saleability == "FOR_SALE" ? book.SaleInfo.ListPrice.Amount.ToString() + " " + book.SaleInfo.ListPrice.CurrencyCode : "książka niedostępna w sprzedaży",
                        ImageLink = book.VolumeInfo.ReadingModes.Image ? book.VolumeInfo.ImageLinks.Thumbnail : "/images/brak_okladki.png",
                        ISBN = book.VolumeInfo.IndustryIdentifiers is not null && book.VolumeInfo.IndustryIdentifiers.Any(x => x.Type.Equals("ISBN_13"))
                        ? book.VolumeInfo.IndustryIdentifiers.FirstOrDefault(x => x.Type.Equals("ISBN_13"))?.Identifier ?? "brak danych"
                        : book.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type.Equals("ISBN_10"))?.Identifier ?? "brak danych",
                        FormatAvailable = new List<string>
                            {
                                book.AccessInfo.Epub.IsAvailable ? book.AccessInfo.Epub.AcsTokenLink : null,
                                book.AccessInfo.Pdf.IsAvailable ? book.AccessInfo.Pdf.AcsTokenLink : null
                            }.Where(link => link != null).ToList()
                };
                    listBook.Add(newBook);
                }
                return listBook; 
            }
            else
            {
                throw new Exception("Nieprawidłowa odpowiedź API");
            }
        }

        public int GetTotalResults(string query)
        {
            string url = $"https://www.googleapis.com/books/v1/volumes?q=?{query}&maxResults=10"; // Pobieramy tylko 1 wynik, ale odczytujemy całkowitą liczbę wyników

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Execute(request, Method.Get);

            if (!response.IsSuccessful)
            {
                throw new Exception("Nieprawidłowa odpowiedź API");
            }

            var contentJson = response.Content;
            GoogleBaseSearchResponse googleResponse = JsonConvert.DeserializeObject<GoogleBaseSearchResponse>(contentJson);

            if (googleResponse?.Items == null || googleResponse.Items.Count == 0)
            {
                return 0;
            }

            return Math.Min(googleResponse.TotalItems, 40);
        }

        public List<Book> AdvancedSearch(string title, string autors, string publisher, string isbn)
        {
            string query = "";

            if (title != null)
            {
                query += $"intitle:{title}";
            }
            if (autors != null)
            {
                if (query != "")
                {
                    query += ",";
                }
                query += $"inauthor:{autors}";
            }
            if (publisher != null)
            {
                if (query != "")
                {
                    query += ",";
                }
                query += $"inpublisher:{publisher}";
            }
            if (isbn != null)
            {
                if (query != "")
                {
                    query += ",";
                }
                query += $"isbn:{isbn}";
            }

            string url = $"https://www.googleapis.com/books/v1/volumes?q={query}";
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Execute(request, Method.Get);

            if (response.IsSuccessful)
            {
                var contentJson = response.Content;
                GoogleBaseSearchResponse googleBaseSearchResponse = JsonConvert.DeserializeObject<GoogleBaseSearchResponse>(contentJson);
                List<Book> listBook = new List<Book>();

                foreach (var book in googleBaseSearchResponse.Items ?? new List<Item>())
                {
                    var newBook = new Book()
                    {
                        Id = book.Id,
                        Title = book.VolumeInfo.Title,
                        Authors = book.VolumeInfo.Authors is null ? null : string.Join(", ", book.VolumeInfo.Authors),
                        PublishedDate = book.VolumeInfo.PublishedDate is null ? null : DateOnly.Parse(
                            book.VolumeInfo.PublishedDate.Contains('-') ?
                            book.VolumeInfo.PublishedDate.ToString() : book.VolumeInfo.PublishedDate + "-01-01").ToString(),
                        PageCount = book.VolumeInfo.PageCount,
                        Publisher = book.VolumeInfo.Publisher is not null ? book.VolumeInfo.Publisher : "brak danych",
                        PrintType = book.VolumeInfo.PrintType,
                        Description = book.VolumeInfo.Description,
                        Price = book.SaleInfo.Saleability == "FOR_SALE" ? book.SaleInfo.ListPrice.Amount.ToString() + " " + book.SaleInfo.ListPrice.CurrencyCode : "książka niedostępna w sprzedaży",
                        ImageLink = book.VolumeInfo.ReadingModes.Image ? book.VolumeInfo.ImageLinks.Thumbnail : "/images/brak_okladki.png",
                        ISBN = book.VolumeInfo.IndustryIdentifiers is not null && book.VolumeInfo.IndustryIdentifiers.Any(x => x.Type.Equals("ISBN_13"))
                        ? book.VolumeInfo.IndustryIdentifiers.FirstOrDefault(x => x.Type.Equals("ISBN_13"))?.Identifier ?? "brak danych"
                        : book.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type.Equals("ISBN_10"))?.Identifier ?? "brak danych"
                };
                    listBook.Add(newBook);
                }
                return listBook;
            }
            else
            {
                throw new Exception("Nieprawidłowa odpowiedź API");
            }
        }

    }
}
