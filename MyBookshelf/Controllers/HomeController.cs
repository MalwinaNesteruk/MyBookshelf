using Microsoft.AspNetCore.Mvc;
using MyBookshelf.Models;
using System.Diagnostics;

namespace MyBookshelf.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
