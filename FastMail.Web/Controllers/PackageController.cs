using FastMail.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastMail.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly ILogger<PackageController> _logger;

        public PackageController(ILogger<PackageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
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