using Marval.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marval.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
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