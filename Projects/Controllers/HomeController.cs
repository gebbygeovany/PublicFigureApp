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
        public IActionResult IndexPost()
        {
            //IEnumerable<People> objList = _db.Peoples; 
            var data = _db.Peoples.Where(x => x.Category == HttpContext.Request.Form["category"].ToString() && x.Region == HttpContext.Request.Form["region"].ToString());
            return View(data);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(People obj)
        {
            if (ModelState.IsValid)
            {
                _db.Peoples.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Peoples.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Peoples.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Peoples.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //GET-Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Peoples.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(People obj)
        {
            if (ModelState.IsValid)
            {
                _db.Peoples.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
