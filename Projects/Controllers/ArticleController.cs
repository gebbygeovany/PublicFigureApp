using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Controllers
{

    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? id)
        {
            ViewData["PeopleId"] = id;
            var data = _db.Articles.Where(x => x.PeopleId == id);
            return View(data);
        }
        
    }
}
