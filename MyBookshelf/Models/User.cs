using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookshelf.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
/*        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }*/
        public string? ImageLink { get; set; }
        public string? Description { get; set; }
    }
}
