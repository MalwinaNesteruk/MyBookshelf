namespace MyBookshelf.Models
{
    public class GoogleBaseSearchResponse
    {
        public List<Item> Items{ get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
        public AccessInfo AccessInfo { get; set; }
        public SaleInfo SaleInfo { get; set; }
    }

    public class SaleInfo
    {
        public string Saleability { get; set; } //tu jest "for_sale" lub "not_for_sale"
        public ListPrice ListPrice { get; set; }
    }

    public class ListPrice
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class AccessInfo
    {
        public AccessInfoFormat Epub { get; set; }
        public AccessInfoFormat Pdf { get; set; }
    }

    public class AccessInfoFormat
    {
        public bool IsAvailable { get; set; }
        public string AcsTokenLink { get; set; }
    }

    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string PublishedDate { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public string PrintType { get; set; }
        public string Description { get; set; }
        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
        public ReadingModes ReadingModes { get; set; }
        public ImageLinks ImageLinks { get; set; }
    }

    public class ImageLinks
    {
        public string Thumbnail { get; set; }
    }

    public class ReadingModes
    {
        public bool Image { get; set; }
    }

    public class IndustryIdentifier
    {
        public string Type { get; set; }
        public string Identifier { get; set; }

    }
}
