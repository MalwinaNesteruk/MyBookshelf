using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBookshelf.Models;
using System.Drawing.Printing;
using System.Security.Claims;

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
        public ActionResult ShowAllUsers(int page = 1, int pageSize = 8)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserSearchResponse userSearchResponse = new UserSearchResponse();

/*            List<User> users = userDbContext.Users
            .Where(u => u.Id != userId)
            .OrderBy(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();*/

            List<User> users = userDbContext.Users
                            .Where(u => u.Id != userId)
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
        public IActionResult LoadMoreUsers(int page, int pageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserSearchResponse userSearchResponse = new UserSearchResponse();

            var users = userDbContext.Users
                                .Where(u => u.Id != userId)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize + 1)
                                .ToList();
            bool hasMore = users.Count > pageSize;
            users = users.Take(pageSize).ToList();
            userSearchResponse.Users = users;
            userSearchResponse.HasMore = hasMore;

            return Json(userSearchResponse);
        }

        [HttpGet]
        //Route("/{userName}")]
        public IActionResult ShowProfile()//string userName)
        {
            string userName = "pierdzibrzdek";
            var user = userDbContext.Users.First(x => x.UserName == userName);
            return View("AnotherUserProfile", user);
        }
    }
}
