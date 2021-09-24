using Microsoft.AspNetCore.Mvc;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Controllers
{
    public class ManagePeopleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ManagePeopleController(ApplicationDbContext db)
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
            ViewData["Category"] = HttpContext.Request.Form["Category"].ToString();
            ViewData["Region"] = HttpContext.Request.Form["Region"].ToString();

            IEnumerable<People> objList = _db.Peoples;

            return View(objList);
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
    }
}
