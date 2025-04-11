using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBookshelf.Models;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyBookshelf.Controllers
{
    public class AnotherUserController : Controller
    {
        private UserDbContext userDbContext;

        public AnotherUserController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        [HttpGet]
        public ActionResult ShowAllUsers(string query = "", int page = 1, int pageSize = 8)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserSearchResponse userSearchResponse = new UserSearchResponse();
            ViewBag.Query = query;

            var usersQuery = userDbContext.Users
                .Where(u => u.Id != userId);

            if (!string.IsNullOrEmpty(query))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(query));
            }

            List<User> users = usersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .ToList();

            bool hasMore = users.Count > pageSize;
            users = users.Take(pageSize).ToList();

            userSearchResponse.Users = users;
            userSearchResponse.HasMore = hasMore;

            return View("AllUserProfiles", userSearchResponse);
        }

        [HttpGet]
        public IActionResult LoadMoreUsers(int page, int pageSize, string query = "")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usersQuery = userDbContext.Users
                .Where(u => u.Id != userId);

            if (!string.IsNullOrEmpty(query))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(query));
            }

            var users = usersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .ToList();

            bool hasMore = users.Count > pageSize;
            users = users.Take(pageSize).ToList();

            return Json(new
            {
                users = users.Select(u => new {
                    userName = u.UserName,
                    imageLink = u.ImageLink
                }),
                hasMore = hasMore
            });
        }

        [HttpGet]
        [Route("AnotherUser/ShowProfile/{userName}")]
        public IActionResult ShowProfile(string userName)
        {
            var user = userDbContext.Users.First(x => x.UserName == userName);

            return View("AnotherUserProfile", user);
        }
    }
}
