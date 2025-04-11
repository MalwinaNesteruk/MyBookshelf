namespace MyBookshelf.Models
{
    public class UserSearchResponse
    {
        public List<User> Users { get; set; }
        public bool HasMore { get; set; }
        public string ErrorMessage { get; set; }
    }
}
