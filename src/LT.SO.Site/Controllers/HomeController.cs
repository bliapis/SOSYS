using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using LT.SO.Site.Models;
using Microsoft.AspNetCore.Hosting;

namespace LT.SO.Site.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(
            IConfiguration configuration,
            IHostingEnvironment env)
            : base(configuration) { }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "NOADMIN")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        [Authorize(Policy = "Simplao")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
