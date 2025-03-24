using Microsoft.AspNetCore.Mvc;

namespace MyBookshelf.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult ShowProfile()
        {
            return View("UserProfile");
        }
    }
}
