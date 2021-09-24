using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET-Index
        public IActionResult Index()
        {
            IEnumerable<People> objList = _db.Peoples;
            return View(objList);
        }
        //POST-Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost()
        {
            ViewData["Category"] = HttpContext.Request.Form["Category"].ToString();
            ViewData["Region"] = HttpContext.Request.Form["Region"].ToString();

            IEnumerable<People> objList = _db.Peoples;

            return View(objList);
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
