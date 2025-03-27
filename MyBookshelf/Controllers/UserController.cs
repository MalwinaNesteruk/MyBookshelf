using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookshelf.Models;

namespace MyBookshelf.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext userDbContext;


        public UserController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        [HttpGet]
        [Route("User/myprofile")]
        public IActionResult ShowMyProfile()
        {
            var userName = User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = userDbContext.Users.First(x => x.UserName == userName);
            return View("UserProfile", user);
        }        
        
        [HttpGet]
        [Route("User/{userName}")]
        public IActionResult ShowProfile(string userName)
        {
            userName = "pierdzibrzdek";
            var user = userDbContext.Users.First(x => x.UserName == userName);
            return View("UserProfile", user);
        }

        [HttpPost]
        public IActionResult UpdateDescription([FromBody] UserProfileUpdateModel model)
        {
            // Pobranie nowego opisu
            string description = model.Description;

            // Pobranie aktualnie zalogowanego użytkownika
            var user = userDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (user == null)
            {
                return NotFound(new { message = "Nie znaleziono użytkownika." });
            }

            // Zapisanie nowego opisu do bazy
            user.Description = description;
            userDbContext.SaveChanges(); // Zapisz zmiany w bazie

            return Ok(new { message = "Opis został zaktualizowany" });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Określamy, gdzie chcemy zapisać plik
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagesUser", file.FileName);

                // Zapisujemy plik na serwerze
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var user = userDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user == null)
                {
                    return NotFound(new { message = "Nie znaleziono użytkownika." });
                }

                user.ImageLink = file.FileName;
                userDbContext.SaveChanges();

                return Ok(new { message = "Obrazek został zaktualizowany." });
            }

            return BadRequest("Nie udało się przesłać pliku.");
        }
    }
}
