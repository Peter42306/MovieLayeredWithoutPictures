using Microsoft.AspNetCore.Mvc;
//using MovieLayeredWithoutPictures.Models;
using System.Diagnostics;

namespace MovieLayeredWithoutPictures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
